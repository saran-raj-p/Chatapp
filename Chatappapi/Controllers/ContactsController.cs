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
        public async Task<IActionResult> CreateContact(Contacts model)
        {
            var res = await _contactsRepository.createcontact(model);
            return Ok();
        }

        [HttpGet("getContact")]
        public async Task<IActionResult> GetContact(Contacts model)
        {
            var res = await _contactsRepository.getcontact(model);
            return Ok();
        }
    }
}
