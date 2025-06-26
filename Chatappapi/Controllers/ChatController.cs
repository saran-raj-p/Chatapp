using Chatappapi.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Chatappapi.Controllers
{
    public class ChatController : Controller
    {
        private readonly IChat _Chat;
        public ChatController(IChat chat)
        {
            _Chat = chat;
        }
        [HttpPost("sendMessage")]
        public async Task<IActionResult> send(Guid fromId, Guid toId, string Message)
        {
            try
            {
                if (fromId == null || toId == null || Message == null)
                {
                    return BadRequest();
                }
                else
                {
                    var result = await _Chat.sendMessage(fromId, toId, Message);
                    if (result == true)
                    {
                        return Ok("Message Sent");
                    }
                    else
                    {
                        return NotFound("Message not Sent");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Server Error");
            }
        }
    }
}
