using AutoMapper;
using BFCAI.Nesyan.Application.Abstraction.Models.Caregivers;
using BFCAI.Nesyan.Application.Abstraction.Services.Caregivers;
using BFCAI.Nesyan.Domain.Contracts;
using BFCAI.Nesyan.Domain.Entities.Primary.Caregivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BFCAI.Nesyan.Application.Services.Caregivers
{
    public class CaregiverService(IUnitOfWork UnitOfWork, IMapper Mapper) : ICaregiverService
    {
        public async Task<IEnumerable<CaregiverToReturnDto>> GetCaregiversAsync()
        {
            var repo = UnitOfWork.GetRepository<Caregiver, int>();
            var caregivers = await repo.GetAllAsync();
            return Mapper.Map<IEnumerable<CaregiverToReturnDto>>(caregivers);
        }

        public async Task<CaregiverToReturnDto> GetCaregiverAsync(int id)
        {
            var repo = UnitOfWork.GetRepository<Caregiver, int>();
            var caregiver = await repo.Get(id);
            if (caregiver is null) throw new Exception("Caregiver not found");
            return Mapper.Map<CaregiverToReturnDto>(caregiver);
        }

        public async Task<CaregiverToReturnDto> CreateCaregiverAsync(CaregiverToCreateDto caregiverToCreate)
        {
            var repo = UnitOfWork.GetRepository<Caregiver, int>();

            var existingCaregivers = await repo.GetAllAsync();
            if (existingCaregivers.Any(d => d.NationalId == caregiverToCreate.NationalId))
                throw new Exception("NationalId is already registered.");
            if (existingCaregivers.Any(d => d.Email.Equals(caregiverToCreate.Email, StringComparison.OrdinalIgnoreCase)))
                throw new Exception("Email is already registered.");
            if (existingCaregivers.Any(d => d.UserName.Equals(caregiverToCreate.UserName, StringComparison.OrdinalIgnoreCase)))
                throw new Exception("UserName is already taken.");

            var caregiver = Mapper.Map<Caregiver>(caregiverToCreate);
            caregiver.Password = BCrypt.Net.BCrypt.HashPassword(caregiver.Password);
            caregiver.CreatedOn = DateTime.UtcNow;
            caregiver.CreatedBy = caregiver.UserName;
            caregiver.LastModifiedOn = DateTime.UtcNow;
            caregiver.LastModifiedBy = caregiver.UserName;

            await repo.AddAsync(caregiver);
            await UnitOfWork.CompleteAsync();

            return Mapper.Map<CaregiverToReturnDto>(caregiver);
        }

        public async Task UpdateCaregiverAsync(CaregiverToReturnDto caregiverToUpdate)
        {
            var repo = UnitOfWork.GetRepository<Caregiver, int>();
            var caregiver = await repo.Get(caregiverToUpdate.Id);
            if (caregiver is null) throw new Exception("Caregiver not found");

            Mapper.Map(caregiverToUpdate, caregiver);
            caregiver.LastModifiedOn = DateTime.UtcNow;
            caregiver.LastModifiedBy = caregiver.UserName;

            repo.Update(caregiver);
            await UnitOfWork.CompleteAsync();
        }

        public async Task DeleteCaregiverAsync(int id)
        {
            var repo = UnitOfWork.GetRepository<Caregiver, int>();
            var caregiver = await repo.Get(id);
            if (caregiver is null) throw new Exception("Caregiver not found");

            repo.Delete(caregiver);
            await UnitOfWork.CompleteAsync();
        }
    }
}
