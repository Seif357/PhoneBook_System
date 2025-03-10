
using Domain_Core.Entities;
using System.Linq.Expressions;
using Application.DTOs;
using Application.Mappings;
using Phonebook.Application.Interfaces;

namespace Application.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        
        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }
        public async Task<IEnumerable<ContactDto>> GetAllContactsAsync()
        {
            return (await _contactRepository.GetAllContactsAsync()).Select(c => c.ToDto());
        }

        public async Task AddContactAsync(CreateContactDto createContactDto)
        {
            var contact = createContactDto.ToDomain();

            if (await _contactRepository.PhoneNumberExistsAsync(contact.PhoneNumber))
            {
                throw new ArgumentException("Phone number already exists.");
            }

            if (await _contactRepository.EmailExistsAsync(contact.Email))
            {
                throw new ArgumentException("Email already exists.");
            }

            await _contactRepository.AddContactAsync(contact);
        }

        public async Task DeleteContactAsync(int id)
        {
            if (!await _contactRepository.ContactExistsAsync(id))
            {
                throw new KeyNotFoundException("Contact not found.");
            }
            await _contactRepository.DeleteContactAsync(id);
        }

        public async Task<IEnumerable<Contact>> SearchContactsAsync(string searchTerm)
        {
            var normalizedTerm = searchTerm?.Trim().ToLower() ?? string.Empty;

            Expression<Func<Contact, bool>> searchExpression = contact => 
                contact.Name.ToLower().Contains(normalizedTerm) ||
                contact.PhoneNumber.Contains(normalizedTerm) ||
                contact.Email.ToLower().Contains(normalizedTerm);

            return await _contactRepository.SearchContactsAsync(searchExpression);
        }
    }
}
