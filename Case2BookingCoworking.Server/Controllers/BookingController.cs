using Case2BookingCoworking.Contracts.Requests;
using Case2BookingCoworking.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Case2BookingCoworking.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IAudienceService _audienceService;
        public BookingController(IAudienceService audienceService) 
        { 
            _audienceService = audienceService;
        }
        [HttpGet("GetBookedAudiences")]
        public async Task<IActionResult> GetBookedAudiences(CancellationToken cancellationToken)
        {
            var result = await _audienceService.GetBookedAudiences(cancellationToken);
            return Ok(result.Value);
        }
        [HttpGet("CheckBookAudience")]
        public async Task<IActionResult> CheckBookAudience(OrderAudienceRequest audienceRequest, Guid userId,CancellationToken cancellationToken)
        {
            var result = await _audienceService.CheckBookAudience(audienceRequest, userId, cancellationToken);
            var audience = result.Value;
            return Ok(audience);
        }
        [HttpPost("AddAudience")]
        public async Task<IActionResult> AddNewAudience(CreateAudienceRequest createAudienceRequest, CancellationToken cancellationToken)
        {
           var result = await _audienceService.AddNewAudience(createAudienceRequest, cancellationToken);
            if (result.IsError)
                return BadRequest(result.Errors);
            return Ok(result.Value);
        }
        [HttpDelete("RemoveAudience")]
        public async Task<IActionResult> RemoveAudience(Guid audienceId, UpdateBookedAudiencesRequest updateRequest, CancellationToken cancellationToken)
        {
            return BadRequest();
        }


    }
}
