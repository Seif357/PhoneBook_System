// In IContactRepository.cs

using System.Linq.Expressions;
using Domain_Core.Entities;

public interface IContactRepository
{
    Task<IEnumerable<Contact>> GetAllContactsAsync();

    Task AddContactAsync(Contact contact);
    Task DeleteContactAsync(int id);
    
    Task<IEnumerable<Contact>> SearchContactsAsync(Expression<Func<Contact, bool>> predicate);
    Task<bool> PhoneNumberExistsAsync(string contactPhoneNumber);
    Task<bool> ContactExistsAsync(int id);
    Task<bool> EmailExistsAsync(string contactEmail);
}