﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Users;
using Passenger.Infrastructure.Services;

namespace Passenger.Api.Controllers
{
    public class AccountController : ApiControllerBase
    {
        public AccountController(ICommandDispatcher commandDispatcher)
            : base(commandDispatcher)
        {
            
        }

        // TODO
        [HttpPut("")]
        [Route("password")]
        public async Task<IActionResult> Put([FromBody] ChangeUserPassword command)
        {
            //await _userService.RegisterAsync(request.Email, request.Username, request.Password);
            await CommandDispatcher.DispatchAsync(command);

            return NoContent();
        }
    }
}