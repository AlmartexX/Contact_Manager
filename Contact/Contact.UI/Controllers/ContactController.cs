using Contact.BLL.DTO;
using Contact.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Contact.UI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        public ContactController(IContactService service)
        {
            _contactService = service
                     ?? throw new ArgumentNullException();

        }

        [HttpGet]
        public async Task<ActionResult<ContactDTO>> GetContactsAsync()
        {
            var contacts = await _contactService.GetContactsAsync();
            if (contacts == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No contacts in database.");
            }

            return StatusCode(StatusCodes.Status200OK, contacts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContactDTO>> GetContactByIdAsync(int id)
        {
            var contact = await _contactService.GetContactByIdAsync(id);
            if (contact == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, $"No events found for id: {id}");
            }

            return StatusCode(StatusCodes.Status200OK, contact);

        }

        [HttpPost]
        public async Task<ActionResult> CreateContactAsync(CreateContactDTO newContact)
        {
            var contact = await _contactService.CreateContactAsync(newContact);
            if (contact == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, $"{newContact.Name} could not be added.");
            }

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateContactAsync(ContactDTO updatedContact, int id)
        {
            var contact = await _contactService.UpdateContactAsync(updatedContact, id);

            return Ok(contact);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteContactAsync(int id)
        {
            await _contactService.DeleteContactAsync(id);

            return NoContent();
        }
    }
}
