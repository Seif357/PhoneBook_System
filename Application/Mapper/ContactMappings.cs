// Application/Mappings/ContactMappings.cs
using Domain_Core.Entities;
using Application.DTOs;

namespace Application.Mappings
{
    public static class ContactMappings
    {
        public static ContactDto ToDto(this Contact contact)
        {
            return new ContactDto
            {
                Id = contact.Id,
                Name = contact.Name,
                PhoneNumber = contact.PhoneNumber,
                Email = contact.Email
            };
        }

        public static Contact ToDomain(this CreateContactDto dto)
        {
            return new Contact
            {
                Name = dto.Name,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email
            };
        }
    }
}