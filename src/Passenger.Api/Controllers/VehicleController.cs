using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Services;

namespace Passenger.Api.Controllers
{
    public class VehicleController : ApiControllerBase
    {
        private readonly IVehicleProvider _vehicleProvider;

        public VehicleController(ICommandDispatcher commandDispatcher, IVehicleProvider vehicleProvider) : base(commandDispatcher)
        {
            _vehicleProvider = vehicleProvider;
        }

        public async Task<IActionResult> Get()
        {
            var vehicles = await _vehicleProvider.BrowseAsync();

            return Json(vehicles);
        }
    }
}