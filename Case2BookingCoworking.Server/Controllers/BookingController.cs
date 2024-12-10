using Case2BookingCoworking.Application.Abstract.Services;
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
        private readonly IOrderService _orderService;
        public BookingController(IAudienceService audienceService, IOrderService orderService) 
        { 
            _audienceService = audienceService;
            _orderService = orderService;
        }
        private Guid ParseUserGuid()
        {
            var userClaim = HttpContext.User.Claims.Where(c => c.Type == "UserId").First().Value;
            return Guid.Parse(userClaim);

        }
        [HttpPost("CreateNewOrder")]
        public async Task<IActionResult> CreateOrder(OrderAudienceRequest orderRequest,CancellationToken cancellationToken)
        {
            var userId = ParseUserGuid();
            var result = await _orderService.CreateOrder(orderRequest, userId, cancellationToken);
            if (result.IsError)
                return BadRequest(result);
            return Ok(result.Value);
        }

        [HttpGet("GetBookedAudiences")]
        public async Task<IActionResult> GetBookedAudiences(CancellationToken cancellationToken)
        {
            var result = await _audienceService.GetBookedAudiences(cancellationToken);
            return Ok(result.Value);
        }
        [HttpPost("CheckBookAudience")]
        public async Task<IActionResult> CheckBookAudience(OrderAudienceRequest audienceRequest, CancellationToken cancellationToken)
        {
            var userId = ParseUserGuid();
            var result = await _audienceService.CheckBookAudience(audienceRequest, userId, cancellationToken);

            if (result.IsError)
                return BadRequest(result);

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
