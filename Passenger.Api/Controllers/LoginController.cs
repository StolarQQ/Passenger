using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
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

        public LoginController(ICommandDispatcher commandDispatcher, IMemoryCache cache) : base(commandDispatcher)
        {
            _cache = cache;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Login command)
        {
            // TokenId == "Key"
            command.Tokenid = Guid.NewGuid();
            await DispatchAsync(command);
            var jwt = _cache.GetJwt(command.Tokenid);

            return Json(jwt);
        }
    }
}