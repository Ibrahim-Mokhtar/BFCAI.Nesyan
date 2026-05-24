using BFCAI.Nesyan.Application.Abstraction.Models.Patients;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BFCAI.Nesyan.Application.Abstraction.Services.Patients
{
    public interface IFamilyMembersService
    {
        Task<IEnumerable<FamilyMemberDto>> GetFamilyMembersByPatientIdAsync(int patientId);
        Task<FamilyMemberDto?> GetFamilyMemberByIdAsync(int id);
        Task<FamilyMemberDto> CreateFamilyMemberAsync(FamilyMemberCreateDto dto);
        Task<FamilyMemberDto> UpdateFamilyMemberAsync(int id, FamilyMemberUpdateDto dto);
        Task<bool> DeleteFamilyMemberAsync(int id);
    }
}
