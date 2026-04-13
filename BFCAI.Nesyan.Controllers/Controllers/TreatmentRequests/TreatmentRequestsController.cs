using BFCAI.Nesyan.Application.Abstraction.Models.TreatmentRequests;
using BFCAI.Nesyan.Application.Abstraction.Services.TreatmentRequests;
using BFCAI.Nesyan.Controllers.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace BFCAI.Nesyan.Controllers.Controllers.TreatmentRequests
{
    public class TreatmentRequestsController(ITreatmentRequestService Service) : BaseApiController
    {
        [HttpPost]
        public async Task<ActionResult<TreatmentRequestToReturnDto>> CreateRequest(TreatmentRequestToCreateDto dto)
        {
            var result = await Service.CreateRequestAsync(dto);
            return Ok(result);
        }

        [HttpGet("doctor/{doctorId}/pending")]
        public async Task<ActionResult<IEnumerable<TreatmentRequestToReturnDto>>> GetPendingRequests(int doctorId)
        {
            var requests = await Service.GetDoctorPendingRequestsAsync(doctorId);
            return Ok(requests);
        }

        [HttpPatch("{id}/accept")]
        public async Task<ActionResult> AcceptRequest(int id)
        {
            try
            {
                await Service.AcceptRequestAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}/reject")]
        public async Task<ActionResult> RejectRequest(int id)
        {
            try
            {
                await Service.RejectRequestAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
