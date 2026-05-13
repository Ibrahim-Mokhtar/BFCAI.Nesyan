using BFCAI.Nesyan.Application.Abstraction.Models.Patients;
using BFCAI.Nesyan.Application.Abstraction.Models.MindGames;

using BFCAI.Nesyan.Application.Abstraction.Services.Patients;
using BFCAI.Nesyan.Domain.Contracts;
using BFCAI.Nesyan.Domain.Entities.Primary.Patients;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BFCAI.Nesyan.Domain.Entities.MindGames;
using BFCAI.Nesyan.Domain.Entities.Relations.MindGames;

namespace BFCAI.Nesyan.Application.Services.Patients
{
    public class PatientService(IUnitOfWork UnitOfWork, IMapper Mapper) : IPatientService
    {
        public async Task UpdatePatientStageAsync(int patientId, int newStage)
        {
            var repo = UnitOfWork.GetRepository<Patient, int>();
            var patient = await repo.Get(patientId);

            if (patient == null)
                throw new Exception("Patient not found");

            patient.CurrentStage = (AlzheimerStage)newStage;
            patient.LastModifiedOn = DateTime.UtcNow;

            repo.Update(patient);
            await UnitOfWork.CompleteAsync();
        }

        public async Task<PatientFullProfileDto> GetPatientProfileAsync(int patientId)
        {
            var patientRepo = UnitOfWork.GetRepository<Patient, int>();
            var patient = await patientRepo.Get(patientId);
            if (patient == null) throw new Exception("Patient not found");

            var pgRepo = UnitOfWork.GetRepository<MindGameSession, int>();
            var gameRepo = UnitOfWork.GetRepository<MindGame, int>();

            var allPG = await pgRepo.GetAllAsync(false);
            var patientGames = allPG.Where(pg => pg.PatientId == patientId).ToList();

            var gameDtos = Mapper.Map<List<PatientMindGameDto>>(patientGames);
            var allGames = await gameRepo.GetAllAsync(false);
            foreach (var g in gameDtos)
            {
                var gameEntity = allGames.FirstOrDefault(x => x.Id == g.MindGameId);
                if (gameEntity != null) g.MindGame = Mapper.Map<MindGameDto>(gameEntity);
            }

            var profileDto = Mapper.Map<PatientFullProfileDto>(patient);
            profileDto.AssignedGames = gameDtos;

            return profileDto;
        }

        public async Task<IEnumerable<PatientToReturnDto>> GetPatientsAsync()
        {
            var repo = UnitOfWork.GetRepository<Patient, int>();
            var patients = await repo.GetAllAsync();
            return Mapper.Map<IEnumerable<PatientToReturnDto>>(patients);
        }

        public async Task<PatientToReturnDto> GetPatientAsync(int id)
        {
            var repo = UnitOfWork.GetRepository<Patient, int>();
            var patient = await repo.Get(id);
            if (patient is null) throw new Exception("Patient not found");
            return Mapper.Map<PatientToReturnDto>(patient);
        }

        public async Task<PatientToReturnDto> CreatePatientAsync(PatientToCreateDto patientToCreate)
        {
            var repo = UnitOfWork.GetRepository<Patient, int>();

            var existingPatients = await repo.GetAllAsync();
            if (existingPatients.Any(p => p.NationalId == patientToCreate.NationalId))
                throw new Exception("NationalId is already registered.");
            if (existingPatients.Any(p => p.Email.Equals(patientToCreate.Email, StringComparison.OrdinalIgnoreCase)))
                throw new Exception("Email is already registered.");
            if (existingPatients.Any(p => p.UserName.Equals(patientToCreate.UserName, StringComparison.OrdinalIgnoreCase)))
                throw new Exception("UserName is already taken.");

            var patient = Mapper.Map<Patient>(patientToCreate);
            patient.Password = BCrypt.Net.BCrypt.HashPassword(patient.Password);
            patient.CreatedOn = DateTime.UtcNow;
            patient.CreatedBy = patient.UserName;
            patient.LastModifiedOn = DateTime.UtcNow;
            patient.LastModifiedBy = patient.UserName;

            await repo.AddAsync(patient);
            await UnitOfWork.CompleteAsync();

            return Mapper.Map<PatientToReturnDto>(patient);
        }

        public async Task UpdatePatientAsync(PatientToReturnDto patientToUpdate)
        {
            var repo = UnitOfWork.GetRepository<Patient, int>();
            var patient = await repo.Get(patientToUpdate.Id);
            if (patient is null) throw new Exception("Patient not found");

            Mapper.Map(patientToUpdate, patient);
            patient.LastModifiedOn = DateTime.UtcNow;
            patient.LastModifiedBy = patient.UserName ?? "System";

            repo.Update(patient);
            await UnitOfWork.CompleteAsync();
        }

        public async Task DeletePatientAsync(int id)
        {
            var repo = UnitOfWork.GetRepository<Patient, int>();
            var patient = await repo.Get(id);
            if (patient is null) throw new Exception("Patient not found");

            repo.Delete(patient);
            await UnitOfWork.CompleteAsync();
        }
    }
}
