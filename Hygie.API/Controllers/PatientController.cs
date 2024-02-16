﻿using System;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Hygie.App.Commands.Patient;
using Hygie.App.Queries.Patient;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hygie.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        public async Task<IActionResult> SetHospitalExitDate([FromBody] DateTime exitDate)
        {
            try
            {
                string userId = this.User.Claims.First(c => c.Type == "UserId")!.Value;
                var command = new SetHospitalExitDateCommand();
                command.ExitDate = exitDate;
                command.UserId = userId;
                return Ok(await _mediator.Send(command));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: 500);
            }
        }

        [HttpGet("HospitalExitDate")]
        [ProducesDefaultResponseType(typeof(DateTime?))]
        public async Task<IActionResult> HospitalExitDate()
        {
            try
            {
                string userId = this.User.Claims.First(c => c.Type == "UserId")!.Value;
                var querry = new GetHospitilExitDateQuery(userId);
                
                return Ok(await _mediator.Send(querry));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, statusCode: 500);
            }
        }
    }
}

