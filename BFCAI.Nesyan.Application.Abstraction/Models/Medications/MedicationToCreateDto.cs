using System;
using System.ComponentModel.DataAnnotations;

namespace BFCAI.Nesyan.Application.Abstraction.Models.Medications
{
    public class MedicationToCreateDto
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Dosage { get; set; } = null!;
        [Required]
        public string Frequency { get; set; } = null!;
        [Required]
        public string ScheduleInstructions { get; set; } = null!;
        public string? Notes { get; set; }

        [Required]
        public int PatientId { get; set; }
        [Required]
        public int DoctorId { get; set; }
    }
}
