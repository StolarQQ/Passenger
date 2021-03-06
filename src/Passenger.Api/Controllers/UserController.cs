﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Users;
using Passenger.Infrastructure.Services;

namespace Passenger.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ApiControllerBase
    {
        private readonly IUserService _userService;
        //private readonly ICommandDispatcher _commandDispatcher;

        public UserController(IUserService userService,
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

        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            var users = await _userService.BrowseAsync();
            if (users == null)
            {
                return NotFound();
            }

            return new JsonResult(users);
        }

        [HttpGet("google")]
        public async Task<IActionResult> GetGoogle()
        {
            // Redirect test
            await _userService.BrowseAsync();
            return new RedirectResult("http://www.google.pl");
        }

        // POST
        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]CreateUser command)
        {
            //await _userService.RegisterAsync(request.Email, request.Username, request.Password);
            await DispatchAsync(command);

            return Created("CreatedUser", $"User {command.Email} was created"); // TODO Created how work
        }
    }
}
