using System.ComponentModel.DataAnnotations;

namespace BFCAI.Nesyan.Application.Abstraction.Models.TreatmentRequests
{
    public class TreatmentRequestToCreateDto
    {
        [Range(1, int.MaxValue)]
        public int PatientId { get; set; }

        [Range(1, int.MaxValue)]
        public int DoctorId { get; set; }

        [Range(1, int.MaxValue)]
        public int RelativeId { get; set; }

        public DateTime? RequestDate { get; set; }
        public string? Notes { get; set; }
    }
}
