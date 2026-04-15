using System;

namespace BFCAI.Nesyan.Application.Abstraction.Models.Patients
{
    public class PatientToReturnDto
    {
        public int Id { get; set; }
        public string FName { get; set; } = null!;
        public string LName { get; set; } = null!;
        public int Age { get; set; }
        public string Gender { get; set; } = null!;
        public int CurrentStage { get; set; }
        public string CurrentStageName { get; set; } = null!;
    // Adding optional dates for LastVisit if needed
        public DateTime? LastVisit { get; set; }
    }
}
