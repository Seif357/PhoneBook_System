using System.Linq.Expressions;
using Domain_Core.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly ApplicationDbContext _context;

        public ContactRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Contact>> GetAllContactsAsync()
        {
            return await _context.Contacts.ToListAsync();
        }
        

        public async Task AddContactAsync(Contact contact)
        {
            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteContactAsync(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact != null)
            {
                _context.Contacts.Remove(contact);
                await _context.SaveChangesAsync();
            }
        }

        public async  Task<IEnumerable<Contact>> SearchContactsAsync(Expression<Func<Contact, bool>> predicate)

        {
            return await _context.Contacts
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<bool> PhoneNumberExistsAsync(string contactPhoneNumber)
        {
            return await _context.Contacts
                .AnyAsync(c => c.PhoneNumber == contactPhoneNumber);
        }

        public async Task<bool> EmailExistsAsync(string contactEmail)
        {
            return await _context.Contacts
                .AnyAsync(c => c.Email == contactEmail);
        }
        public async Task<bool> ContactExistsAsync(int id)
        {
            return await _context.Contacts.AnyAsync(c => c.Id == id);
        }
    }
}