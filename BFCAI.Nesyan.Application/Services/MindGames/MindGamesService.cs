using AutoMapper;
using BFCAI.Nesyan.Application.Abstraction.Models.MindGames;
using BFCAI.Nesyan.Application.Abstraction.Services.MindGames;
using BFCAI.Nesyan.Domain.Contracts;
using BFCAI.Nesyan.Domain.Entities.MindGames;
using BFCAI.Nesyan.Domain.Entities.Primary.Doctors;
using BFCAI.Nesyan.Domain.Entities.Primary.Patients;
using BFCAI.Nesyan.Domain.Entities.Relations.MindGames;
using BFCAI.Nesyan.Domain.Entities.Relations.Primary;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BFCAI.Nesyan.Application.Services.MindGames
{
    public class MindGamesService(IUnitOfWork UnitOfWork, IMapper Mapper) : IMindGamesService
    {
        private async Task<string> SaveFileAsync(IFormFile file, string folderName)
        {
            if (file == null || file.Length == 0) return string.Empty;
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", folderName);
            if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return $"/Uploads/{folderName}/{uniqueFileName}";
        }

        public async Task<IEnumerable<MindGameDto>> GetGameCatalogAsync()
        {
            var repo = UnitOfWork.GetRepository<MindGame, int>();
            var games = await repo.GetAllAsync(false);
            return Mapper.Map<IEnumerable<MindGameDto>>(games);
        }

        public async Task<MindGameDto?> GetMindGameByIdAsync(int id)
        {
            var repo = UnitOfWork.GetRepository<MindGame, int>();
            var game = await repo.Get(id);
            if (game == null) return null;
            return Mapper.Map<MindGameDto>(game);
        }

        public async Task<MindGameDto> CreateMindGameAsync(MindGameCreateDto dto)
        {
            var repo = UnitOfWork.GetRepository<MindGame, int>();
            var game = Mapper.Map<MindGame>(dto);
            
            if (dto.Image != null)
            {
                game.Image = await SaveFileAsync(dto.Image, "MindGames");
            }

            await repo.AddAsync(game);
            await UnitOfWork.CompleteAsync();

            return Mapper.Map<MindGameDto>(game);
        }

        public async Task<MindGameDto> UpdateMindGameAsync(int id, MindGameUpdateDto dto)
        {
            var repo = UnitOfWork.GetRepository<MindGame, int>();
            var game = await repo.Get(id);
            if (game == null) throw new Exception("Mind game not found.");

            Mapper.Map(dto, game);

            if (dto.Image != null)
            {
                game.Image = await SaveFileAsync(dto.Image, "MindGames");
            }

            repo.Update(game);
            await UnitOfWork.CompleteAsync();

            return Mapper.Map<MindGameDto>(game);
        }

        public async Task<bool> DeleteMindGameAsync(int id)
        {
            var repo = UnitOfWork.GetRepository<MindGame, int>();
            var game = await repo.Get(id);
            if (game == null) return false;

            repo.Delete(game);
            await UnitOfWork.CompleteAsync();
            return true;
        }

        public async Task<IEnumerable<PatientMindGameDto>> GetPatientGamesAsync(int patientId)
        {
            var repo = UnitOfWork.GetRepository<MindGameSession, int>();
            var gameRepo = UnitOfWork.GetRepository<MindGame, int>();

            var allAssignments = await repo.GetAllAsync(false);
            var patientAssignments = allAssignments.Where(pg => pg.PatientId == patientId).ToList();

            var dtos = Mapper.Map<List<PatientMindGameDto>>(patientAssignments);
            var allGames = await gameRepo.GetAllAsync(false);

            foreach (var dto in dtos)
            {
                var game = allGames.FirstOrDefault(g => g.Id == dto.MindGameId);
                if (game != null)
                {
                    dto.MindGame = Mapper.Map<MindGameDto>(game);
                }
            }
            return dtos;
        }

        public async Task AssignGameToPatientAsync(int patientId, int gameId, AssignMindGameDto dto)
        {
            var repo = UnitOfWork.GetRepository<MindGameSession, int>();
            var doctorRepo = UnitOfWork.GetRepository<Doctor, int>();
            var patientRepo = UnitOfWork.GetRepository<Patient, int>();
            var gameRepo = UnitOfWork.GetRepository<MindGame, int>();

            var allAssignments = await repo.GetAllAsync(false);
            var existing = allAssignments.FirstOrDefault(x => x.PatientId == patientId && x.MindGameId == gameId);
            if (existing != null) throw new Exception("Game already assigned to this patient.");

            if (await patientRepo.Get(patientId) is null)
                throw new Exception("Patient not found.");

            if (await gameRepo.Get(gameId) is null)
                throw new Exception("Mind game not found.");

            if (await doctorRepo.Get(dto.DoctorId) is null)
                throw new Exception("Doctor not found.");

            var assignment = new MindGameSession
            {
                DoctorId = dto.DoctorId,
                PatientId = patientId,
                MindGameId = gameId,
                AddedDate = DateTime.UtcNow,
                StartDate = dto.StartDate,
                Frequency = dto.Frequency,
                CreatedOn = DateTime.UtcNow
            };

            await repo.AddAsync(assignment);
            await UnitOfWork.CompleteAsync();
        }

        public async Task RemoveGameFromPatientAsync(int patientId, int gameId)
        {
            var repo = UnitOfWork.GetRepository<MindGameSession, int>();

            var allAssignments = await repo.GetAllAsync(false);
            var assignment = allAssignments.FirstOrDefault(x => x.PatientId == patientId && x.MindGameId == gameId);
            if (assignment == null) throw new Exception("Game assignment not found.");

            repo.Delete(assignment);
            await UnitOfWork.CompleteAsync();
        }

        public async Task<PatternGameRecordDto> SubmitPatternGameResultAsync(int patientId, PatternGameRecordToCreateDto dto)
        {
            var patientRepo = UnitOfWork.GetRepository<Patient, int>();
            var recordRepo = UnitOfWork.GetRepository<PatternGameRecord, int>();

            if (await patientRepo.Get(patientId) is null)
                throw new Exception("Patient not found.");

            var record = Mapper.Map<PatternGameRecord>(dto);
            record.PatientId = patientId;

            await recordRepo.AddAsync(record);
            await UnitOfWork.CompleteAsync();

            return Mapper.Map<PatternGameRecordDto>(record);
        }

        public async Task<IEnumerable<PatternGameRecordDto>> GetPatientPatternGameHistoryAsync(int patientId)
        {
            var patientRepo = UnitOfWork.GetRepository<Patient, int>();
            var recordRepo = UnitOfWork.GetRepository<PatternGameRecord, int>();

            if (await patientRepo.Get(patientId) is null)
                throw new Exception("Patient not found.");

            var allRecords = await recordRepo.GetAllAsync(false);
            var patientRecords = allRecords.Where(r => r.PatientId == patientId).OrderByDescending(r => r.DateTime).ToList();

            return Mapper.Map<IEnumerable<PatternGameRecordDto>>(patientRecords);
        }
    }
}
