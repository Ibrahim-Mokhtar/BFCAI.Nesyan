using BFCAI.Nesyan.Application.Abstraction.Models.Medications;
using BFCAI.Nesyan.Application.Abstraction.Services.Medications;
using BFCAI.Nesyan.Controllers.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BFCAI.Nesyan.Controllers.Controllers.Medications
{
    public class MedicationsController(IMedicationService MedicationService) : BaseApiController
    {
        [HttpGet("patient/{patientId}")]
        public async Task<ActionResult<IEnumerable<MedicationToReturnDto>>> GetPatientMedications(int patientId)
        {
            try
            {
                var meds = await MedicationService.GetPatientMedicationsAsync(patientId);
                return Ok(meds);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<MedicationToReturnDto>> AddMedication([FromBody] MedicationToCreateDto dto)
        {
            try
            {
                var med = await MedicationService.AddMedicationAsync(dto);
                return CreatedAtAction(nameof(GetPatientMedications), new { patientId = med.PatientId }, med);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMedication(int id)
        {
            try
            {
                await MedicationService.DeleteMedicationAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
