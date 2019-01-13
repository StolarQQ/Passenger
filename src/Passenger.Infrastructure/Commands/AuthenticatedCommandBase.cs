using System;

namespace Passenger.Infrastructure.Commands
{
    public class AuthenticatedCommandBase : IAuthenticateCommand
    {
        public Guid UserId { get; set; }
    }
}