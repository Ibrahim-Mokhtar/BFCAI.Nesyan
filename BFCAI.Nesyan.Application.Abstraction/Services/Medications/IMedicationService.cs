using BFCAI.Nesyan.Application.Abstraction.Models.Medications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BFCAI.Nesyan.Application.Abstraction.Services.Medications
{
    public interface IMedicationService
    {
        Task<IEnumerable<MedicationToReturnDto>> GetPatientMedicationsAsync(int patientId);
        Task<MedicationToReturnDto> AddMedicationAsync(MedicationToCreateDto dto);
        Task DeleteMedicationAsync(int id);
    }
}
