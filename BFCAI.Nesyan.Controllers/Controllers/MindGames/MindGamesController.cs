using BFCAI.Nesyan.Application.Abstraction.Models.MindGames;
using BFCAI.Nesyan.Application.Abstraction.Services.MindGames;
using BFCAI.Nesyan.Controllers.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BFCAI.Nesyan.Controllers.Controllers.MindGames
{
    public class MindGamesController(IMindGamesService MindGamesService) : BaseApiController
    {
        [HttpGet("catalog")]
        public async Task<ActionResult<IEnumerable<MindGameDto>>> GetGameCatalog()
        {
            try
            {
                var games = await MindGamesService.GetGameCatalogAsync();
                return Ok(games);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("patient/{patientId}")]
        public async Task<ActionResult<IEnumerable<PatientMindGameDto>>> GetPatientGames(int patientId)
        {
            try
            {
                var games = await MindGamesService.GetPatientGamesAsync(patientId);
                return Ok(games);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("patient/{patientId}/assign/{gameId}")]
        public async Task<ActionResult> AssignGameToPatient(int patientId, int gameId)
        {
            try
            {
                await MindGamesService.AssignGameToPatientAsync(patientId, gameId);
                return Ok(new { Message = "Game assigned successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("patient/{patientId}/remove/{gameId}")]
        public async Task<ActionResult> RemoveGameFromPatient(int patientId, int gameId)
        {
            try
            {
                await MindGamesService.RemoveGameFromPatientAsync(patientId, gameId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
