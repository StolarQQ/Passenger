using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Users;
using Passenger.Infrastructure.DTO;
using Passenger.Infrastructure.Services;
using Passenger.Infrastructure.Settings;

namespace Passenger.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ApiControllerBase
    {
        private readonly IUserService _userService;
        //private readonly ICommandDispatcher _commandDispatcher;

        public UsersController(IUserService userService,
            ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
            _userService = userService;
            //_commandDispatcher = commandDispatcher;
        }

        // GET // Before UserDTO result
        //[Authorize(Policy = "admin")]
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

        [HttpGet("{all}")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            if (users == null)
            {
                return NotFound();
            }

            return new JsonResult(users);
        }

        // POST
        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]CreateUser command)
        {
            //await _userService.RegisterAsync(request.Email, request.Username, request.Password);
            await CommandDispatcher.DispatchAsync(command);

            return Created($"users/{command.Email}", new { command.Email }); // TODO Created how work
        }


        [HttpGet("")]
        public async Task Test()
        {
            await Task.CompletedTask;
        }
    }
}
