using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace BFCAI.Nesyan.Controllers.Controllers.Doctors
{
    public class DoctorCreationRequest
    {
        [Required]
        public string NationalId { get; set; } = null!;
        [Required]
        public string FName { get; set; } = null!;
        [Required]
        public string LName { get; set; } = null!;
        [Required]
        public string UserName { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string Gender { get; set; } = null!;
        [Required]
        public string Country { get; set; } = null!;
        
        public string City { get; set; } = null!;
        public int Age { get; set; }

        [Required]
        public IFormFile GraduationDegreeFile { get; set; } = null!;
        
        [Required]
        public IFormFile MedicalAssociationCardFile { get; set; } = null!;
    }
}
