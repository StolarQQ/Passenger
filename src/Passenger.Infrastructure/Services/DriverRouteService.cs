using System;
using System.Threading.Tasks;
using AutoMapper;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.Extenstions;

namespace Passenger.Infrastructure.Services
{
    public class DriverRouteService : IDriverRouteService
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IMapper _mapper;
        private readonly IRouteManger _routeManger;

        public DriverRouteService(IDriverRepository driverRepository,
             IMapper mapper, IRouteManger routeManger)
        {
            _driverRepository = driverRepository;
            _mapper = mapper;
            _routeManger = routeManger;
        }

        public async Task AddAsync(Guid userId, string name, double startLatitude, double startLongitude, double endLatitude,
            double endLongitude)
        {
            var driver = await _driverRepository.GetOrFailAsync(userId);
            // Mimic sample google maps Address
            var startAddress = await _routeManger.GetAddressAsync(startLatitude, startLongitude);
            var endAddress = await _routeManger.GetAddressAsync(endLatitude, endLongitude);
            // Start node for route
            var startNode = Node.Create(startAddress, startLatitude, startLongitude);
            var endNode = Node.Create(endAddress, startLongitude, endLongitude);
            var distance = _routeManger.CalculateDistance(startLatitude, startLongitude, endLatitude, endLongitude);
            driver.AddRoute(name, startNode, endNode, distance);
            await _driverRepository.UpdateAsync(driver);
        }

        public async Task DeleteAsync(Guid userId, string name)
        {
            var driver = await _driverRepository.GetOrFailAsync(userId);
            driver.DeleteRoute(name);
            await _driverRepository.DeleteAsync(driver);
        }
    }
}