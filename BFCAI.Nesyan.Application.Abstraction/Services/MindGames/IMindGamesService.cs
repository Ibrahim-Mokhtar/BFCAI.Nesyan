using BFCAI.Nesyan.Application.Abstraction.Models.MindGames;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BFCAI.Nesyan.Application.Abstraction.Services.MindGames
{
    public interface IMindGamesService
    {
        Task<IEnumerable<MindGameDto>> GetGameCatalogAsync();
        Task<IEnumerable<PatientMindGameDto>> GetPatientGamesAsync(int patientId);
        Task AssignGameToPatientAsync(int patientId, int gameId);
        Task RemoveGameFromPatientAsync(int patientId, int gameId);
    }
}
