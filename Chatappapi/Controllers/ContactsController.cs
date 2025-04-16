using Chatappapi.Interface;
using Chatappapi.Model;
using Microsoft.AspNetCore.Mvc;

namespace Chatappapi.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IContactsRepository _contactsRepository;

        public ContactsController(IContactsRepository contactsRepository)
        {
            _contactsRepository = contactsRepository;
        }


        [HttpPost("createContact")]
        public async Task<IActionResult> CreateContact([FromBody]Contacts model)

        {
            try
            {
                if (model == null)
                {
                    return BadRequest(new { Message = "Contact Data is required" });
                }
                var res = await _contactsRepository.createcontact(model);
                if (res == null)
                {
                    return BadRequest(new { Message = "Contact not created. Email might not be registered." });
                }

                return Ok(new { Message = "Contact created successfully", Data = res });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error", details = ex.Message });

            }
        }

        [HttpGet("getContact")]
        public async Task<IActionResult> GetContact([FromQuery]Contacts model)
        {
            try
            {
                var res = await _contactsRepository.getcontact(model);
                //if (res == null)
                //{
                //    return BadRequest(new { Message = "No Contact Found" });
                //}
                return Ok(new { Data = res });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error", details = ex.Message });
            }
        }
    }
}
