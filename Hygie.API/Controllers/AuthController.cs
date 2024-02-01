using Hygie.App.Commands.Auth;
using Hygie.App.DTOs;
using Hygie.Infrastructure.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hygie.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
           
        }


        [HttpPost("Login")]
        [ProducesDefaultResponseType(typeof(AuthResponseDTO))]
        public async Task<IActionResult> Login([FromBody] AuthCommand command)
        {
            try
            {
                return Ok(await _mediator.Send(command));
            }
            catch(BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: 500);
            }
        }

        [HttpPost("ResetPassword")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
        {
            try
            {
                return Ok(await _mediator.Send(command));
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: 500);
            }

        }

        [HttpPost("ChangePassword")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
        {
            try
            {
                return Ok(await _mediator.Send(command));
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: 500);
            }

        }

        [HttpPost("ConfirmEmail")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailCommand command)
        {
            try
            {
                return Ok(await _mediator.Send(command));
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: 500);
            }

        }
    }
}