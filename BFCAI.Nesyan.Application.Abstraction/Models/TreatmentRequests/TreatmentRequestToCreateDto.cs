using System.ComponentModel.DataAnnotations;

namespace BFCAI.Nesyan.Application.Abstraction.Models.TreatmentRequests
{
    public class TreatmentRequestToCreateDto
    {
        [Required]
        public string MemoryLossFrequency { get; set; } = null!;
        [Required]
        public string ConfusionFrequency { get; set; } = null!;
        [Required]
        public string LanguageProblems { get; set; } = null!;
        [Required]
        public string MoodChanges { get; set; } = null!;
        [Required]
        public string RepetitiveBehavior { get; set; } = null!;

        [Required]
        public int PatientId { get; set; }
        
        [Required]
        public int DoctorId { get; set; }

        public int? RelativeId { get; set; }
    }
}
