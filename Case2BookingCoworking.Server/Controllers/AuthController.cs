using Case2BookingCoworking.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Case2BookingCoworking.Application.Abstract.Services;
using Case2BookingCoworking.Contracts.Requests;
namespace Case2BookingCoworking.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ApiController
    {
        private readonly IUserService userService;

        private const string UserDTO = "UserDTO";
        private const string SessionId = "SessionId";
        public AuthController(IUserService userService, ILogger<AuthController> logger) : base(logger)
        {
            this.userService = userService;
        }

        [HttpPost("beginRegistration")]
        public async Task<ActionResult> BeginRegistration(UserRegisterRequest registerRequest, CancellationToken cancellationToken)
        {
            HttpContext.Session.SetString(UserDTO, JsonSerializer.Serialize(registerRequest));
            HttpContext.Session.SetString(SessionId, HttpContext.Session.Id);


            var sendingMailResult = await userService.SendEmailVerificationCode(registerRequest.Email, cancellationToken);

            if (sendingMailResult.IsError)
            {
                var message = "Internal Service Error.";
                var details = $"An error has occured during sending message to: {registerRequest.Email}";
                _logger.LogWarning($"{message} {details}");
                _logger.LogWarning(JsonSerializer.Serialize(sendingMailResult.Errors));

                return StatusCode
                (
                    500,
                    new
                    {
                        message,
                        details
                    }
                );
            }
            var result = $"The code has been successfully sent to the mail: {registerRequest.Email}";
            _logger.LogInformation(result);
            return Ok(result);
        }

        [HttpPost("endRegistration")]
        public async Task<ActionResult> EndRegistration(string code, CancellationToken cancellationToken)
        {
            if (HttpContext.Session.Id == HttpContext.Session.GetString(SessionId))
            {
                var serializedUser = HttpContext.Session.GetString(UserDTO);
                var userRegistrationRequest = JsonSerializer.Deserialize<UserRegisterRequest>(serializedUser);

                var registrationResult = await userService.RegisterIfVerifiedAsync(userRegistrationRequest, code, cancellationToken);

                if (registrationResult.IsError)
                {
                    _logger.LogWarning(JsonSerializer.Serialize(registrationResult.Errors));
                    return BadRequest(registrationResult);
                }

                return Ok("Registration successfull");
            }

            return Forbid();
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(UserLoginRequest loginRequest)
        {
            var userToken = await userService.Login(loginRequest.Email, loginRequest.Password);

            if (userToken.IsError == true)
            {
                _logger.LogWarning("Error has occured during generation JWT token");
                _logger.LogWarning(JsonSerializer.Serialize(userToken.Errors));
                return BadRequest(userToken.Errors);
            }

            HttpContext.Response.Cookies.Append("bivis-bober", userToken.Value);
            _logger.LogInformation($"The session {HttpContext.Session.Id} successfully recieved an authorization token");
            return Ok(userToken);
        }
    }
}

