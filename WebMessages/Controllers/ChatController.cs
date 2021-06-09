using System;
using Microsoft.AspNetCore.Mvc;
using WebMessages.Models;

namespace WebMessages.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class ChatController : Controller {
        private static Chat chat = new Chat();
        private static Random random = new Random();
        
        [HttpPost("random-chat")]
        public IActionResult RandomChat()
        {
            chat = Chat.GetRandomChat(random);
            return Ok(chat);
        }

        [HttpGet("get-user-by-email")]
        public IActionResult GetUserByEmail([FromQuery] string email) {
            if (!chat.DoesEmailExist(email))
                return NotFound();
            return Ok(chat.UsersDict[email]);
        }
        
        [HttpGet("get-all-users")]
        public IActionResult GetUsers() {
            return Ok(chat.GetUserList());
        }
        
        /*[HttpGet("get-messages-sender-receiver")]
        public IActionResult GetMessagesSenderReceiver([FromQuery] int senderId, [FromQuery] int receiverId) {
            if (senderId >=)
        }*/
        
        [HttpGet]
        public IActionResult Index() {
            return Ok(chat);
        }
    }
}