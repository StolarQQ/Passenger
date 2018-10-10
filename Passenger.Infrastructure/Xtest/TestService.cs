using System.Linq.Expressions;

namespace Passenger.Infrastructure.Services
{
    public class TestService
    {
        public void Test()
        {
            var us = new UserService(new IMemoryUserRepository());
            var otherdatabase = new UserService(new IMemoryUserRepository());

            us.Register("elo","stolar","123");
        }
    }
}