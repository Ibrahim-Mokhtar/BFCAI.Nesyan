using BFCAI.Nesyan.Application.Abstraction.Services.Patients;
using BFCAI.Nesyan.Application.Abstraction.Models.Patients;
using BFCAI.Nesyan.Controllers.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace BFCAI.Nesyan.Controllers.Controllers.Patients
{
    public class PatientsController(IPatientService PatientService) : BaseApiController
    {
        [HttpPatch("{id}/stage")]
        public async Task<ActionResult> UpdatePatientStage(int id, [FromBody] int newStage)
        {
            try
            {
                await PatientService.UpdatePatientStageAsync(id, newStage);
                return NoContent(); // 204
            }
            catch (System.Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}/profile")]
        public async Task<ActionResult<PatientFullProfileDto>> GetPatientProfile(int id)
        {
            try
            {
                var profile = await PatientService.GetPatientProfileAsync(id);
                return Ok(profile);
            }
            catch (System.Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
