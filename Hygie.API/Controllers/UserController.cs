using Hygie.App.Commands.User;
using Hygie.App.DTOs;
using Hygie.App.Queries.User;
using Hygie.Infrastructure.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Hygie.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Create")]
        [AllowAnonymous]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<ActionResult> CreateUser(CreateUserCommand command)
        {
            try
            {
                return Ok(await _mediator.Send(command));
            }
            catch(ValidationException ex)
            {
                return BadRequest(ex.Errors.Values);
            }
            catch (Exception ex)
            {
                return Problem("Internal server error",ex.Message);
            }
        }

        [HttpGet("GetAll")]
        [ProducesDefaultResponseType(typeof(List<UserResponseDTO>))]
        public async Task<IActionResult> GetAllUserAsync()
        {
            return Ok(await _mediator.Send(new GetUserQuery()));
        }

        [HttpGet("GetAllByRole/{roleName}")]
        [Authorize(Roles = "Nurse, Admin")]
        [ProducesDefaultResponseType(typeof(List<UserResponseDTO>))]
        public async Task<IActionResult> GetAllUserByRoleAsync(string roleName)
        {
            return Ok(await _mediator.Send(new GetAllUsersByRoleQuery() { RoleName = roleName}));
        }

        [HttpDelete("Delete/{userId}")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var result = await _mediator.Send(new DeleteUserCommand() { Id = userId });
            return Ok(result);
        }

        [HttpGet("GetUserDetails/{userId}")]
        [ProducesDefaultResponseType(typeof(UserDetailsResponseDTO))]
        public async Task<IActionResult> GetUserDetails(string userId)
        {
            var result = await _mediator.Send(new GetUserDetailsQuery() { UserId = userId });
            return Ok(result);
        }

        [HttpGet("GetUserDetailsByUserName/{userName}")]
        [ProducesDefaultResponseType(typeof(UserDetailsResponseDTO))]
        public async Task<IActionResult> GetUserDetailsByUserName(string userName)
        {
            var result = await _mediator.Send(new GetUserDetailsByUserNameQuery() { UserName = userName });
            return Ok(result);
        }

        [HttpPost("AssignRoles")]
        [ProducesDefaultResponseType(typeof(int))]

        public async Task<ActionResult> AssignRoles(AssignUsersRoleCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("EditUserRoles")]
        [ProducesDefaultResponseType(typeof(int))]

        public async Task<ActionResult> EditUserRoles(UpdateUserRolesCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("GetAllUserDetails")]
        [ProducesDefaultResponseType(typeof(UserDetailsResponseDTO))]
        public async Task<IActionResult> GetAllUserDetails()
        {
            var result = await _mediator.Send(new GetAllUsersDetailsQuery());
            return Ok(result);
        }

        [HttpPost("EditProfilePicture")]
        [Consumes("multipart/form-data")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<IActionResult>UpdateProfilePicture([FromForm] UpdateProfilePictureCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("UpdateAdress")]
        [Authorize(Roles = "patient, nurse, pharmacist")]
        public async Task<IActionResult> UpdateAdress([FromBody] UpdateAdressCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("UpdatePhoneNumber")]
        [Authorize(Roles = "patient, nurse, pharmacist")]
        public async Task<IActionResult> UpdatePhoneNumber([FromBody] UpdatePhoneNumberCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }


        [HttpPut("EditUserProfile/{id}")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<ActionResult> EditUserProfile(string id, [FromBody] EditUserProfileCommand command)
        {
            if (id == command.Id)
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
