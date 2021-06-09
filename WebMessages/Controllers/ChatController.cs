using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebMessages.Models;
using WebMessages.Utilities;

namespace WebMessages.Controllers
{
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
        /// <returns> 200 code with randomized chat. </returns>
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
        /// <param name="email"> Email to find user. </param>
        /// <returns> 404 or 200 code with the user. </returns>
        [HttpGet("get-user-by-email")]
        public IActionResult GetUserByEmail([FromQuery] string email)
        {
            if (!chat.DoesEmailExist(email))
                return NotFound();
            return Ok(chat.UsersDict[email]);
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns> 202 code with all users. </returns>
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
        /// <returns> 404 or 202 code with message list. </returns>
        [HttpGet("get-messages-sender-receiver")]
        public IActionResult GetMessagesSenderReceiver([FromQuery] string senderId, [FromQuery] string receiverId)
        {
            if (!chat.DoesEmailExist(senderId) || !chat.DoesEmailExist(receiverId))
            {
                return NotFound();
            }

            List<MessageObject> messageObjects = chat.Messages
                .Where(msg => msg.SenderId == senderId && msg.ReceiverId == receiverId).ToList();

            return Ok(messageObjects);
        }

        /// <summary>
        /// Get all messages with selected <paramref name="senderId"/>.
        /// </summary>
        /// <param name="senderId"> SenderId of the messages. </param>
        /// <returns> 404 or 202 code with message list. </returns>
        [HttpGet("get-messages-sender")]
        public IActionResult GetMessagesSender([FromQuery] string senderId)
        {
            if (!chat.DoesEmailExist(senderId))
            {
                return NotFound();
            }

            List<MessageObject> messageObjects = chat.Messages
                .Where(msg => msg.SenderId == senderId).ToList();

            return Ok(messageObjects);
        }

        /// <summary>
        /// Get all messages with selected <paramref name="receiverId"/>.
        /// </summary>
        /// <param name="receiverId"> ReceiverId of the messages. </param>
        /// <returns> 404 or 202 code with message list. </returns>
        [HttpGet("get-messages-receiver")]
        public IActionResult GetMessagesSenderReceiver([FromQuery] string receiverId)
        {
            if (!chat.DoesEmailExist(receiverId))
            {
                return NotFound();
            }

            List<MessageObject> messageObjects = chat.Messages
                .Where(msg => msg.ReceiverId == receiverId).ToList();

            return Ok(messageObjects);
        }
        
        /// <summary>
        /// Register new user if possible.
        /// </summary>
        /// <param name="user"> User to register. </param>
        /// <returns> 409 or 202 code with registered <paramref name="user"/>. </returns>
        [HttpPost("register-user")]
        public IActionResult RegisterUser([FromBody] User user)
        {
            if (!chat.AddUser(user))
            {
                return Conflict();
            }

            chat.Save(Path);
            return Ok(user);
        }
        
        /// <summary>
        /// Register new message if possible.
        /// </summary>
        /// <param name="messageObject"> MessageObject to register. </param>
        /// <returns> 404 or 202 code with registered <paramref name="messageObject"/>. </returns>
        [HttpPost("register-message")]
        public IActionResult RegisterMessage([FromBody] MessageObject messageObject)
        {
            if (!chat.AddMessage(messageObject))
            {
                return NotFound();
            }

            chat.Save(Path);
            return Ok(messageObject);
        }
        
        
        /// <summary>
        /// Get <paramref name="limit"/> users starting from <paramref name="offset"/> position.
        /// </summary>
        /// <param name="limit"> Number of users to get. </param>
        /// <param name="offset"> Number of users to pass. </param>
        /// <returns> 404 or 202 code with all users. </returns>
        [HttpGet("get-all-users-query")]
        public IActionResult GetUsers([FromQuery] int limit, [FromQuery] int offset)
        {
            if (limit <= 0 || offset < 0)
            {
                return BadRequest();
            }
            
            return Ok(chat.GetUserList(limit, offset));
        }

        /// <summary>
        /// Index page returns chat object.
        /// </summary>
        /// <returns> 202 code with chat object. </returns>
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(chat);
        }
    }
}