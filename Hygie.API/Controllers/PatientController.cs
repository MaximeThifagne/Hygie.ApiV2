using Hygie.App.Commands.Patients;
using Hygie.App.DTOs;
using Hygie.App.Queries.Patients;
using Hygie.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Hygie.API.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PatientController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PatientController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<Patient>> Get()
        {
            return await _mediator.Send(new GetAllPatientQuery());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<Patient> Get(Int64 id)
        {
            return await _mediator.Send(new GetPatientByIdQuery(id));
        }

        [HttpGet("email")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<Patient> GetByEmail(string email)
        {
            return await _mediator.Send(new GetPatientByEmailQuery(email));
        }

        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PatientResponseDTO>> CreatePatient([FromBody] CreatePatientCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }


        [HttpPut("Edit/{id}")]
        public async Task<ActionResult> Edit(int id, [FromBody] EditPatientCommand command)
        {
            try
            {
                if (command.Id == id)
                {
                    var result = await _mediator.Send(command);
                    return Ok(result);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }


        [Authorize(Roles = "Admin, Management")]
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeletePatient(int id)
        {
            try
            {
                string result = string.Empty;
                result = await _mediator.Send(new DeletePatientCommand(id));
                return Ok(result);
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }

    }
}
