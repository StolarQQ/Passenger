using System;

namespace Passenger.Infrastructure.Commands
{
    public interface IAuthenticateCommand : ICommand
    {
        Guid UserId { get; set; }
    }
}