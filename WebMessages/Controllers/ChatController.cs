using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebMessages.Models;
using WebMessages.Utilities;

namespace WebMessages.Controllers
{
    /// <summary>
    /// Main controller for the chat system.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ChatController : Controller
    {
        private const string Path = "chat.json";
        private static Chat chat = SaveLoadChat.Load(Path);
        private static readonly Random random = new Random();

        /// <summary>
        /// Generate chat with random values.
        /// </summary>
        /// <response code="200"> Chat was successfully generated. </response>
        [HttpPost("random-chat")]
        public IActionResult RandomChat()
        {
            chat = Chat.GetRandomChat(random);
            chat.Save(Path);
            return Ok(chat);
        }

        /// <summary>
        /// Get user by their email.
        /// </summary>
        /// <param name="email"> Email to find user with. </param>
        /// <response code="200"> User was returned. </response>
        /// <response code="404"> User with such email was not found. </response>
        [HttpGet("get-user-by-email")]
        public IActionResult GetUserByEmail([FromQuery] string email)
        {
            if (!chat.DoesEmailExist(email))
                return NotFound(new {Message = "User with such email was not found."});
            return Ok(chat.UsersDict[email]);
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <response code="200"> Users were returned. </response>
        [HttpGet("get-all-users")]
        public IActionResult GetUsers()
        {
            return Ok(chat.GetUserList());
        }

        /// <summary>
        /// Get all messages with selected <paramref name="senderId"/> and <paramref name="receiverId"/>.
        /// </summary>
        /// <param name="senderId"> SenderId of the messages. </param>
        /// <param name="receiverId"> ReceiverId of the messages. </param>
        /// <response code="200"> Messages were successfully returned. </response>
        /// <response code="404">
        ///     Users with <paramref name="senderId"/> or <paramref name="receiverId"/> were not found.
        /// </response>
        [HttpGet("get-messages-sender-receiver")]
        public IActionResult GetMessagesSenderReceiver([FromQuery] string senderId, [FromQuery] string receiverId)
        {
            if (!chat.DoesEmailExist(senderId))
                return NotFound(new {Message = "User with such senderId was not found."});
            if (!chat.DoesEmailExist(receiverId))
                return NotFound(new {Message = "User with such receiverId was not found."});


            List<MessageObject> messageObjects = chat.Messages
                .Where(msg => msg.SenderId == senderId && msg.ReceiverId == receiverId).ToList();

            return Ok(messageObjects);
        }

        /// <summary>
        /// Get all messages with selected <paramref name="senderId"/>.
        /// </summary>
        /// <param name="senderId"> SenderId of the messages. </param>
        /// <response code="200"> Messages were successfully returned. </response>
        /// <response code="404"> User with <paramref name="senderId"/> was not found. </response>
        [HttpGet("get-messages-sender")]
        public IActionResult GetMessagesSender([FromQuery] string senderId)
        {
            if (!chat.DoesEmailExist(senderId))
            {
                return NotFound(new {Message = "User with such id was not found."});
            }

            List<MessageObject> messageObjects = chat.Messages
                .Where(msg => msg.SenderId == senderId).ToList();

            return Ok(messageObjects);
        }

        /// <summary>
        /// Get all messages with selected <paramref name="receiverId"/>.
        /// </summary>
        /// <param name="receiverId"> ReceiverId of the messages. </param>
        /// <response code="200"> Messages were successfully returned. </response>
        /// <response code="404"> User with <paramref name="receiverId"/> was not found. </response>
        [HttpGet("get-messages-receiver")]
        public IActionResult GetMessagesSenderReceiver([FromQuery] string receiverId)
        {
            if (!chat.DoesEmailExist(receiverId))
            {
                return NotFound(new {Message = "User with such id was not found."});
            }

            List<MessageObject> messageObjects = chat.Messages
                .Where(msg => msg.ReceiverId == receiverId).ToList();

            return Ok(messageObjects);
        }

        /// <summary>
        /// Register new user if possible.
        /// </summary>
        /// <param name="user"> User to register. </param>
        /// <response code="200"> New user was successfully registered. </response>
        /// <response code="409"> User with such email already exists. </response>
        [HttpPost("register-user")]
        public IActionResult RegisterUser([FromBody] User user)
        {
            if (!chat.AddUser(user))
            {
                return Conflict(new {Message = "User with such email already exists."});
            }

            chat.Save(Path);
            return Ok(user);
        }

        /// <summary>
        /// Register new message if possible.
        /// </summary>
        /// <param name="messageObject"> MessageObject to register. </param>
        /// <response code="200"> New message was successfully added. </response>
        /// <response code="404"> SenderId or ReceiverId were not found. </response>
        [HttpPost("register-message")]
        public IActionResult RegisterMessage([FromBody] MessageObject messageObject)
        {
            if (!chat.AddMessage(messageObject))
            {
                return NotFound(new {Message = "SenderId or ReceiverId were not found."});
            }

            chat.Save(Path);
            return Ok(messageObject);
        }


        /// <summary>
        /// Get <paramref name="limit"/> number of users starting from <paramref name="offset"/> position.
        /// </summary>
        /// <param name="limit" example="1"> Number of users to get. </param>
        /// <param name="offset" example="0"> Number of users to skip. </param>
        /// <response code="200"> Users were successfully returned. </response>
        /// <response code="400">
        ///     <paramref name="limit"/> &lt;= 0 or <paramref name="offset"/> &lt; 0.
        /// </response>
        [HttpGet("get-all-users-query")]
        public IActionResult GetUsers([FromQuery] int limit, [FromQuery] int offset)
        {
            if (limit <= 0)
                return BadRequest(new {Message = "Limit should be > 0"});
            if (offset < 0)
                return BadRequest(new {Message = "Offset should be >= 0"});

            return Ok(chat.GetUserList(limit, offset));
        }

        /// <summary>
        /// Index page returns chat object.
        /// </summary>
        /// <response code="200"> Chat object was returned. </response>
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(chat);
        }
    }
}