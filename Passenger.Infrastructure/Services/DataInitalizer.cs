using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Passenger.Infrastructure.Services
{
    public class DataInitializer : IDataInitializer
    {
        private readonly IUserService _userService;
        private readonly IDriverService _driverService;
        private readonly IDriverRouteService _driverRouteService;

        private readonly ILogger<DataInitializer> _logger;

        public DataInitializer(IUserService userService, ILogger<DataInitializer> logger, IDriverService driverService
            ,IDriverRouteService driverRouteService)
        {
            _userService = userService;
            _logger = logger;
            _driverService = driverService;
            _driverRouteService = driverRouteService;
        }

        public async Task SeedAsync()
        {
            _logger.LogDebug("Initializing data...");
            var tasks = new List<Task>();
            for (int i = 0; i < 10; i++)
            {
                var rnd = new Random();

                var userid = Guid.NewGuid();
                var username = $"user{i}";
                tasks.Add(_userService.RegisterAsync(userid, $"{username}@gmail.com", username, "secret", "user"));
                _logger.LogDebug($"Created a new user '{username}' ");
                tasks.Add(_driverService.CreateAsync(userid));
                tasks.Add(_driverService.SetVehicleAsync(userid, "BMW", "i8"));
                tasks.Add(_driverRouteService.AddAsync(userid, "default", rnd.Next(1,10), rnd.Next(1, 10), rnd.Next(1, 10), rnd.Next(1, 10)));
                tasks.Add(_driverRouteService.AddAsync(userid, "job route", rnd.Next(1, 10), rnd.Next(1, 10), rnd.Next(1, 10), rnd.Next(1, 10)));

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