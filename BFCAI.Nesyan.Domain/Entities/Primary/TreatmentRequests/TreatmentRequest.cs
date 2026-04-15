using BFCAI.Nesyan.Domain.Entities.Common;
using System;

namespace BFCAI.Nesyan.Domain.Entities.Primary.TreatmentRequests
{
    public enum RequestStatus
    {
        Pending = 0,
        Accepted = 1,
        Rejected = 2
    }

    public class TreatmentRequest : BaseAuditableEntity<int>
    {
        // Assessment Data
        public string MemoryLossFrequency { get; set; } = null!;
        public string ConfusionFrequency { get; set; } = null!;
        public string LanguageProblems { get; set; } = null!;
        public string MoodChanges { get; set; } = null!;
        public string RepetitiveBehavior { get; set; } = null!;

        public RequestStatus Status { get; set; } = RequestStatus.Pending;

        // Foreign Keys
        public int PatientId { get; set; }
        public Patient.Patient Patient { get; set; } = null!;

        public int DoctorId { get; set; }
        public Doctor.Doctor Doctor { get; set; } = null!;

        public int? RelativeId { get; set; }
        public Relative.Relative? Relative { get; set; }
    }
}
