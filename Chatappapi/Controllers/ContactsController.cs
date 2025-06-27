using Chatappapi.Interface;
using Chatappapi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Chatappapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : Controller
    {
        private readonly IContactsRepository _contactsRepository;

        public ContactsController(IContactsRepository contactsRepository)
        {
            _contactsRepository = contactsRepository;
        }


        [HttpPost("createContact")]
        public async Task<IActionResult> CreateContact([FromBody]ContactsDTO model)

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
            catch (SqlException ex)
            {
                // Catching the RAISERROR from SQL
                return StatusCode(400, new
                {
                    message = "Failed to create contact",
                    details = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error", details = ex.Message });

            }
        }

        [HttpGet("getContact")]
        public async Task<IActionResult> GetContact([FromQuery]getContact model)
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
