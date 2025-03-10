using Application.DTOs;
using Application.Mappings;
using Domain_Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Phonebook.Application.Interfaces;

namespace API_Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;
        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactDto>>> GetAllContacts()
        {
            try
            {
                var contacts = await _contactService.GetAllContactsAsync();
                return Ok(contacts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving contacts: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<string>> AddContact([FromBody] CreateContactDto createContactDto)
        {
            
            try
            {
                await _contactService.AddContactAsync(createContactDto);
                return Ok("Contact added successfully.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while adding the contact: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> DeleteContact(int id)
        {
            try
            {
                await _contactService.DeleteContactAsync(id);
                return Ok("Contact deleted successfully.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Contact not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the contact: {ex.Message}");
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Contact>>> Search([FromQuery] string term)
        {
            try
            {
                var contacts = await _contactService.SearchContactsAsync(term);
                return Ok(contacts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while searching for contacts: {ex.Message}");
            }
        }
    }
}
