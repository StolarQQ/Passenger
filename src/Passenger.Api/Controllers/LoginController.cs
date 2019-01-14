using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Users;
using Passenger.Infrastructure.Extenstions;

namespace Passenger.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ApiControllerBase
    {
        private readonly IMemoryCache _cache;
        private readonly ILogger<LoginController> _logger;

        public LoginController(ICommandDispatcher commandDispatcher, IMemoryCache cache, ILogger<LoginController> logger) : base(commandDispatcher)
        {
            _cache = cache;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Login command)
        {
            // TokenId == "Key"
            command.Tokenid = Guid.NewGuid();
            await DispatchAsync(command);
            var jwt = _cache.GetJwt(command.Tokenid);
            _logger.LogInformation($"User {command.Email} logged in");

            return Json(jwt);
        }
    }
}