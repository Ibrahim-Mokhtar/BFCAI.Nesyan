using BFCAI.Nesyan.Application.Abstraction.Models.Assessments;
using BFCAI.Nesyan.Application.Abstraction.Models.Medications;
using BFCAI.Nesyan.Application.Abstraction.Models.MindGames;
using System.Collections.Generic;

namespace BFCAI.Nesyan.Application.Abstraction.Models.Patients
{
    public class PatientFullProfileDto
    {
        public int Id { get; set; }
        public string NationalId { get; set; } = null!;
        public string FName { get; set; } = null!;
        public string LName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public int Age { get; set; }
        public string Gender { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
        public int CurrentStage { get; set; }
        public string CurrentStageName { get; set; } = null!;
        public double Height { get; set; }
        public double Weight { get; set; }
        public string BloodType { get; set; } = null!;
        public string ChronicDisease { get; set; } = null!;

        public IEnumerable<MedicationToReturnDto> Medications { get; set; } = new List<MedicationToReturnDto>();
        public IEnumerable<PatientMindGameDto> AssignedGames { get; set; } = new List<PatientMindGameDto>();
        public IEnumerable<AssessmentsToReturnDto> Assessments { get; set; } = new List<AssessmentsToReturnDto>();
    }
}
