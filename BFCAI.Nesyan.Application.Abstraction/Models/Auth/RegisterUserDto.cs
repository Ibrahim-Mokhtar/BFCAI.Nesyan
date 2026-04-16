using BFCAI.Nesyan.Domain.Entities.Primary;
using System.ComponentModel.DataAnnotations;

namespace BFCAI.Nesyan.Application.Abstraction.Models.Auth
{
    public class RegisterUserDto
    {
        [Required]
        [StringLength(14, MinimumLength = 14)]
        public string NationalId { get; set; } = null!;
        [Required]
        public string FName { get; set; } = null!;
        [Required]
        public string LName { get; set; } = null!;
        [Required]
        public string UserName { get; set; } = null!;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        [MinLength(6)]
        public string Password { get; set; } = null!;
        [Required]
        public Gender Gender { get; set; }
        [Required]
        public string Country { get; set; } = null!;
        [Required]
        public string City { get; set; } = null!;
        [Required]
        public int Age { get; set; }
    }
}
