using System;

namespace BFCAI.Nesyan.Application.Abstraction.Models.Medications
{
    public class MedicationToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Dosage { get; set; } = null!;
        public string Frequency { get; set; } = null!;
        public string ScheduleInstructions { get; set; } = null!;
        public string? Notes { get; set; }
        
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
    }
}
