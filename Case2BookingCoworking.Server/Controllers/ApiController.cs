﻿using ErrorOr;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Case2BookingCoworking.Server.Controllers
{
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        protected readonly ILogger<ApiController> _logger;

        protected ApiController(ILogger<ApiController> logger)
        {
            _logger = logger;
        }

        protected ActionResult Problem(List<Error> errors)
        {
            if (errors.Count is 0)
            {
                return Problem();
            }

            if (errors.All(error => error.Type == ErrorType.Validation))
            {
                return ValidationProblem(errors);
            }

            return Problem(errors[0]);
        }

        private ObjectResult Problem(Error error)
        {
            var statusCode = error.Type switch
            {
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Unauthorized => StatusCodes.Status403Forbidden,
                _ => StatusCodes.Status500InternalServerError,
            };

            return Problem(statusCode: statusCode, title: error.Description);
        }

        private ActionResult ValidationProblem(List<Error> errors)
        {
            var modelStateDictionary = new ModelStateDictionary();

            errors.ForEach(error => modelStateDictionary.AddModelError(error.Code, error.Description));

            return ValidationProblem(modelStateDictionary);
        }

        protected ActionResult BadRequestAction(object errors)
        {
            _logger.LogWarning(JsonSerializer.Serialize(errors));

            return BadRequest(errors);
        }
    }
}
