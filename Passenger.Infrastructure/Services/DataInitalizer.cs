using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Passenger.Infrastructure.Services
{
    public class DataInitializer : IDataInitializer
    {
        private readonly IUserService _userService;
        private readonly ILogger<DataInitializer> _logger;

        public DataInitializer(IUserService userService, ILogger<DataInitializer> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            _logger.LogDebug("Initializing data...");
            var tasks = new List<Task>();
            for (int i = 0; i < 10; i++)
            {
                var userid = Guid.NewGuid();
                var username = $"user{i}";
                tasks.Add(_userService.RegisterAsync(userid, $"{username}@gmail.com", username, "secret", "user"));
            }

            for (int i = 0; i < 3; i++)
            {
                var userid = Guid.NewGuid();
                var username = $"admin{i}";
                tasks.Add(_userService.RegisterAsync(userid, $"{username}@gmail.com", username, "secret", "admin"));
            }

            await Task.WhenAll(tasks);
            _logger.LogDebug("Data was initialized");
        }
    }
}