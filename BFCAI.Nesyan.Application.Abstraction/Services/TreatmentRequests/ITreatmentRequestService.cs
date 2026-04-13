using BFCAI.Nesyan.Application.Abstraction.Models.TreatmentRequests;

namespace BFCAI.Nesyan.Application.Abstraction.Services.TreatmentRequests
{
    public interface ITreatmentRequestService
    {
        Task<TreatmentRequestToReturnDto> CreateRequestAsync(TreatmentRequestToCreateDto dto);
        Task<IEnumerable<TreatmentRequestToReturnDto>> GetDoctorPendingRequestsAsync(int doctorId);
        Task AcceptRequestAsync(int requestId);
        Task RejectRequestAsync(int requestId);
    }
}
