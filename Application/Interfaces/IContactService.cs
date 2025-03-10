using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs;
using Domain_Core.Entities;

namespace Phonebook.Application.Interfaces
{
    public interface IContactService
    {
        Task<IEnumerable<ContactDto>> GetAllContactsAsync();
        Task AddContactAsync(CreateContactDto createContactDto);
        Task DeleteContactAsync(int id);
        Task<IEnumerable<Contact>> SearchContactsAsync(string searchTerm);
    }
}