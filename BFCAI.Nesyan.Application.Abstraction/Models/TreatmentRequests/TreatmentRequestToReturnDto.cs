namespace BFCAI.Nesyan.Application.Abstraction.Models.TreatmentRequests
{
    public class TreatmentRequestToReturnDto
    {
        public int Id { get; set; }
        public string Status { get; set; } = null!;
        public DateTime RequestDate { get; set; }
        public string? Notes { get; set; }

        public int PatientId { get; set; }
        public string PatientName { get; set; } = null!;
        public int PatientAge { get; set; }

        public int DoctorId { get; set; }
        public string? DoctorName { get; set; }

        public int RelativeId { get; set; }
        public string? RelativeName { get; set; }
    }
}
