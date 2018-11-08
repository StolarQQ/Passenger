using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Users;
using Passenger.Infrastructure.DTO;
using Passenger.Infrastructure.Services;

namespace Passenger.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICommandDispatcher _commandDispatcher;


        public UsersController(IUserService userService, ICommandDispatcher commandDispatcher)
        {
            _userService = userService;
            _commandDispatcher = commandDispatcher;
        }

        // GET // Before UserDTO result
        [HttpGet("{email}")]
        public async Task<IActionResult> Get(string email)
        {
            var user = await _userService.GetAsync(email);
            if (user == null)
            {
                return NotFound();
            }

            return new JsonResult(user);
        }
             

        // POST

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] CreateUser command)
        {
            //await _userService.RegisterAsync(request.Email, request.Username, request.Password);
            await _commandDispatcher.DispatchAsync(command);

            return Created($"users/{command.Email}", new {command.Email}); // TODO Created how work
        }

        [HttpGet("")]
        public async Task<string> ZZZ()
        {
            return "elo";
        }
        
    }
}
