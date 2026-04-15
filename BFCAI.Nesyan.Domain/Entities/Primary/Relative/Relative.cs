using System;

namespace BFCAI.Nesyan.Domain.Entities.Primary.Relative
{
    public class Relative : User
    {
        public string Relationship { get; set; } = null!;
        
        // Navigation properties
        public int? PatientId { get; set; }
        public Patient.Patient? Patient { get; set; }
    }
}
