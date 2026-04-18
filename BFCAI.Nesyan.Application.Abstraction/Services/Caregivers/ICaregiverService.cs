using BFCAI.Nesyan.Application.Abstraction.Models.Caregivers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BFCAI.Nesyan.Application.Abstraction.Services.Caregivers
{
    public interface ICaregiverService
    {
        Task<IEnumerable<CaregiverToReturnDto>> GetCaregiversAsync();
        Task<CaregiverToReturnDto> GetCaregiverAsync(int id);
        Task<CaregiverToReturnDto> CreateCaregiverAsync(CaregiverToCreateDto caregiverToCreate);
        Task UpdateCaregiverAsync(CaregiverToReturnDto caregiverToUpdate);
        Task DeleteCaregiverAsync(int id);
    }
}
