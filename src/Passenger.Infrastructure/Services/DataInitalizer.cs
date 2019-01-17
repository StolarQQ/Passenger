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
            //var users = await _userService.BrowseAsync();
            //if (users != null)
            //{
            //    return;
            //}

            _logger.LogDebug("Initializing data...");
            //var awaited = new List<Task>();
            for (int i = 0; i < 10; i++)
            {
                var rnd = new Random();

                var userId = Guid.NewGuid();
                var username = $"user{i}";
                await _userService.RegisterAsync(userId, $"{username}@gmail.com", username, "secret", "user");
                _logger.LogDebug($"Created a new user '{username}' ");
                await _driverService.CreateAsync(userId);
                await _driverService.SetVehicleAsync(userId, "BMW", "i8");
                await _driverRouteService.AddAsync(userId, "default", rnd.Next(1, 10), rnd.Next(1, 10), rnd.Next(1, 10), rnd.Next(1, 10));
                await _driverRouteService.AddAsync(userId, "job route", rnd.Next(1, 10), rnd.Next(1, 10), rnd.Next(1, 10), rnd.Next(1, 10));
            }

            for (int i = 0; i < 3; i++)
            {
                var userId = Guid.NewGuid();
                var username = $"admin{i}";
                await _userService.RegisterAsync(userId, $"{username}@gmail.com", username, "secret", "admin");
            }

            // await Task.WhenAll(tasks);

            
        }
    }
}