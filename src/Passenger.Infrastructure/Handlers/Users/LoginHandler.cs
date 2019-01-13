﻿using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Users;
using Passenger.Infrastructure.Extenstions;
using Passenger.Infrastructure.Services;

namespace Passenger.Infrastructure.Handlers.Users
{
    public class LoginHandler : ICommandHandler<Login>
    {
        private readonly IHandler _handler;
        private readonly IUserService _userService;
        private readonly IJwtHandler _jwtHandler;
        private readonly IMemoryCache _cache;

        public LoginHandler(IHandler handler, IMemoryCache cache, IJwtHandler jwtHandler, IUserService userService)
        {
            _handler = handler;
            _userService = userService;
            _jwtHandler = jwtHandler;
            _cache = cache;
        }

        public async Task HandleAsync(Login command)
        => await _handler
            .Run(async () => await _userService.LoginAsync(command.Email, command.Password))
            .Next()
            .Run(async () =>
            {
                var user = await _userService.GetAsync(command.Email);
                var jwt = _jwtHandler.CreateToken(user.UserId, user.Role);
                _cache.SetJwt(command.Tokenid, jwt);
            })
            .ExecuteAsync();

        //public async Task HandleAsync(Login command)
        //{
        //    await _userService.LoginAsync(command.Email, command.Password);
        //    var user = await _userService.GetAsync(command.Email);
        //    var jwt = _jwtHandler.CreateToken(user.UserId, user.Role);
        //    _cache.SetJwt(command.Tokenid, jwt);
        //}
    }
}