using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Drivers;

namespace Passenger.Api.Controllers
{
    [Route("drivers/routes")]
    public class DriverRoutesController : ApiControllerBase
    {
        
        public DriverRoutesController(ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
        }
        
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateDriverRoute command)
        {
            await DispatchAsync(command);

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{name}")]
        public async Task<IActionResult> Delete([FromBody]DeleteDriverRoute command)
        {
            await DispatchAsync(command);

            return NoContent();
        }
    }
}