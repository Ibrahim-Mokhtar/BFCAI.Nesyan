namespace BFCAI.Nesyan.Application.Abstraction.Models.TreatmentRequests
{
    public class TreatmentRequestToReturnDto
    {
        public int Id { get; set; }
        public string MemoryLossFrequency { get; set; } = null!;
        public string ConfusionFrequency { get; set; } = null!;
        public string LanguageProblems { get; set; } = null!;
        public string MoodChanges { get; set; } = null!;
        public string RepetitiveBehavior { get; set; } = null!;
        public string Status { get; set; } = null!;

        public int PatientId { get; set; }
        public string PatientName { get; set; } = null!;
        public int PatientAge { get; set; }
        
        public int DoctorId { get; set; }
        
        public int? RelativeId { get; set; }
        public string? RelativeName { get; set; }
        public string? RelativeRelationship { get; set; }
    }
}
