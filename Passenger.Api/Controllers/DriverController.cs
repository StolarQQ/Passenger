﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Users;
using Passenger.Infrastructure.Services;

namespace Passenger.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DriverController : ApiControllerBase
    {
        private readonly IDriverService _driverService;

        public DriverController(ICommandDispatcher commandDispatcher, IDriverService driverService)
            : base(commandDispatcher)
        {
            _driverService = driverService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var drivers = await _driverService.BrowseAsync();

            return new JsonResult(drivers);
        }

        [HttpGet("{userid}")]
        public async Task<IActionResult> Get(Guid userid)
        {
            var driver = await _driverService.GetAsync(userid);
            if (driver == null)
            {
                return NotFound();
            }

            return new JsonResult(driver);
        }

        // TODO
        [HttpPost]
        public async Task<IActionResult> Put([FromBody] ChangeUserPassword command)
        {
            //await _userService.RegisterAsync(request.Email, request.Username, request.Password);
            await DispatchAsync(command);

            return NoContent();
        }
    }
}