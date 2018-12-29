using System;
using System.Collections.Generic;
using System.Text;

namespace Passenger.Infrastructure.Commands.Users
{
    public class Login : ICommand
    {
        public Guid Tokenid { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
