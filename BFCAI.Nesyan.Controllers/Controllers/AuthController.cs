using BFCAI.Nesyan.Application.Abstraction.Services.Auth;
using BFCAI.Nesyan.Application.Abstraction.Models.Auth;
using BFCAI.Nesyan.Controllers.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BFCAI.Nesyan.Controllers.Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register-patient")]
        public async Task<IActionResult> RegisterPatient([FromBody] RegisterPatientDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterPatientAsync(dto);
            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("register-doctor")]
        public async Task<IActionResult> RegisterDoctor([FromForm] RegisterDoctorDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterDoctorAsync(dto);
            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("register-relative")]
        public async Task<IActionResult> RegisterRelative([FromBody] RegisterRelativeDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterRelativeAsync(dto);
            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("register-caregiver")]
        public async Task<IActionResult> RegisterCaregiver([FromBody] RegisterCaregiverDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterCaregiverAsync(dto);
            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.LoginAsync(dto);
            if (!result.IsSuccess)
                return StatusCode(401, result);

            return Ok(result);
        }
    }
}
