using System.Linq.Expressions;

namespace Passenger.Infrastructure.Services
{
    public class TestService
    {
        public void Test()
        {
            var us = new UserService(new InMemoryUserRepository());
            var otherdatabase = new UserService(new InMemoryUserRepository());

            us.Register("elo","stolar","123");

        }
    }
}