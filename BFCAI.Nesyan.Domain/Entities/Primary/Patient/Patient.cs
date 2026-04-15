using BFCAI.Nesyan.Domain.Entities.Primary.MindGames;
using System;

namespace BFCAI.Nesyan.Domain.Entities.Primary.Patient
{
    public enum AlzheimerStage
    {
        Stage1_Mild = 1,
        Stage2_Moderate = 2,
        Stage3_Severe = 3
    }

    public class Patient : User
    {
        public double Height { get; set; }
        public double Weight { get; set; }
        public string BloodType { get; set; } = null!;
        public string? ChronicDiseases { get; set; }
        
        public AlzheimerStage CurrentStage { get; set; } = AlzheimerStage.Stage1_Mild;

        // Navigation properties
        public ICollection<Relative.Relative> Relatives { get; set; } = new List<Relative.Relative>();
        public ICollection<Doctor.Doctor> Doctors { get; set; } = new List<Doctor.Doctor>();
        public ICollection<TreatmentRequests.TreatmentRequest> TreatmentRequests { get; set; } = new List<TreatmentRequests.TreatmentRequest>();
        public ICollection<PatientMindGame> AssignedGames { get; set; } = new List<PatientMindGame>();
    }
}
