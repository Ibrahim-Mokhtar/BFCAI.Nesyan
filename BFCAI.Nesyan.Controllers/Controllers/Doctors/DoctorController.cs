using BFCAI.Nesyan.Application.Abstraction.Models.Doctors;
using BFCAI.Nesyan.Application.Abstraction.Services.Doctors;
using BFCAI.Nesyan.Controllers.Controllers.Base;
using Microsoft.AspNetCore.Mvc;


namespace BFCAI.Nesyan.Controllers.Controllers.Doctors
{
    public class DoctorController(IDoctorService DoctorService):BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorToReturnDto>>> GetDoctors()
        {
            var doctors = await DoctorService.GetDoctorsAsync();
            return Ok(doctors);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorToReturnDto>> GetDoctor(int id)
        {
            var doctor = await DoctorService.GetDoctorAsync(id);

            if (doctor is null)
                return NotFound($"Doctor with id {id} not found");

            return Ok(doctor);
        }
        [HttpPost]
        public async Task<ActionResult<DoctorToReturnDto>> CreateDoctor(DoctorToCreateDto dto)
        {
            var doctor = await DoctorService.CreateDoctorAsync(dto);
            return CreatedAtAction(
                   nameof(GetDoctor),
                   new { id = doctor.Id },
                   doctor);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDoctor(DoctorToReturnDto dto)
        {
            try
            {
                await DoctorService.UpdateDoctorAsync(dto);
                return NoContent(); // 204
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDoctor(int id)
        {
            try
            {
                await DoctorService.DeleteDoctorAsync(id);
                return NoContent(); // 204
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
