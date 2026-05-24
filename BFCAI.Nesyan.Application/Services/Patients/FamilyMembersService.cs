using AutoMapper;
using BFCAI.Nesyan.Application.Abstraction.Models.Patients;
using BFCAI.Nesyan.Application.Abstraction.Services.Patients;
using BFCAI.Nesyan.Domain.Contracts;
using BFCAI.Nesyan.Domain.Entities.Primary.Patients;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BFCAI.Nesyan.Application.Services.Patients
{
    public class FamilyMembersService(IUnitOfWork UnitOfWork, IMapper Mapper) : IFamilyMembersService
    {
        private async Task<string> SaveFileAsync(IFormFile file, string subFolder)
        {
            if (file == null || file.Length == 0) return string.Empty;
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "FamilyMembers", subFolder);
            if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return $"/Uploads/FamilyMembers/{subFolder}/{uniqueFileName}";
        }

        public async Task<IEnumerable<FamilyMemberDto>> GetFamilyMembersByPatientIdAsync(int patientId)
        {
            var repo = UnitOfWork.GetRepository<FamilyMember, int>();
            var all = await repo.GetAllAsync(false);
            var filtered = all.Where(f => f.PatientId == patientId).ToList();
            return Mapper.Map<IEnumerable<FamilyMemberDto>>(filtered);
        }

        public async Task<FamilyMemberDto> CreateFamilyMemberAsync(FamilyMemberCreateDto dto)
        {
            var patientRepo = UnitOfWork.GetRepository<Patient, int>();
            var patient = await patientRepo.Get(dto.PatientId);
            if (patient == null) throw new Exception("Patient not found.");

            var repo = UnitOfWork.GetRepository<FamilyMember, int>();
            var member = Mapper.Map<FamilyMember>(dto);

            if (dto.Image != null)
            {
                member.ImageUrl = await SaveFileAsync(dto.Image, "Images");
            }
            if (dto.Audio != null)
            {
                member.AudioUrl = await SaveFileAsync(dto.Audio, "Audios");
            }

            member.CreatedOn = DateTime.UtcNow;
            member.CreatedBy = "System";
            member.LastModifiedOn = DateTime.UtcNow;
            member.LastModifiedBy = "System";

            await repo.AddAsync(member);
            await UnitOfWork.CompleteAsync();

            return Mapper.Map<FamilyMemberDto>(member);
        }

        public async Task<FamilyMemberDto> UpdateFamilyMemberAsync(int id, FamilyMemberUpdateDto dto)
        {
            var repo = UnitOfWork.GetRepository<FamilyMember, int>();
            var member = await repo.Get(id);
            if (member == null) throw new Exception("Family member not found.");

            var patientRepo = UnitOfWork.GetRepository<Patient, int>();
            var patient = await patientRepo.Get(dto.PatientId);
            if (patient == null) throw new Exception("Patient not found.");

            Mapper.Map(dto, member);

            if (dto.Image != null)
            {
                member.ImageUrl = await SaveFileAsync(dto.Image, "Images");
            }
            if (dto.Audio != null)
            {
                member.AudioUrl = await SaveFileAsync(dto.Audio, "Audios");
            }

            member.LastModifiedOn = DateTime.UtcNow;
            member.LastModifiedBy = "System";

            repo.Update(member);
            await UnitOfWork.CompleteAsync();

            return Mapper.Map<FamilyMemberDto>(member);
        }

        public async Task<bool> DeleteFamilyMemberAsync(int id)
        {
            var repo = UnitOfWork.GetRepository<FamilyMember, int>();
            var member = await repo.Get(id);
            if (member == null) return false;

            repo.Delete(member);
            await UnitOfWork.CompleteAsync();
            return true;
        }
    }
}
