using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Domain_Core.Entities
{
    [Index(nameof(PhoneNumber), IsUnique = true)]
    [Index(nameof(Email), IsUnique = true)]
    public class Contact
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public required string Name { get; set; } 
        
        [Required]
        [StringLength(20)]
        public required string PhoneNumber { get; set; }
        
        [Required]
        [StringLength(100)]
        [EmailAddress]
        public required string Email { get; set; }
    }
}