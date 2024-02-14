using System;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Hygie.App.Commands.Patient;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hygie.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "patient")]
    public class PatientController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PatientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("SetHospitalExitDate")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<IActionResult> SetHospitalExitDate([FromBody] SetHospitalExitDateCommand command)
        {
            try
            {
                string userId = this.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
                command.UserId = userId;
                return Ok(await _mediator.Send(command));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: 500);
            }
        }
    }
}

