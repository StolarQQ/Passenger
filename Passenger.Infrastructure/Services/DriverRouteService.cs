using System;
using System.Threading.Tasks;
using AutoMapper;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;

namespace Passenger.Infrastructure.Services
{
    public class DriverRouteService : IDriverRouteService
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public DriverRouteService(IDriverRepository driverRepository,
            IUserRepository userRepository, IMapper mapper)
        {
            _driverRepository = driverRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }


        public async Task AddAsync(Guid userId, string name, double startLatitude, double startLongitude, double endLatitude,
            double endLongitude)
        {
            var driver = await _driverRepository.GetAsync(userId);
            if (driver == null)
            {
                throw new Exception($"Driver with user id '{userId}' was not found");
            }

            var start = Node.Create("StartAddress", startLatitude, startLongitude);
            var end = Node.Create("EndAddress", startLongitude, endLongitude);
            driver.AddRoute(name, start, end);
            await _driverRepository.UpdateAsync(driver);
        }

        public async Task DeleteAsync(Guid userid, string name)
        {
            var driver = await _driverRepository.GetAsync(userid);
            if (driver == null)
            {
                throw new Exception($"Driver with user id '{userid}' was not found");
            }

            driver.DeleteRoute(name);
            await _driverRepository.DeleteAsync(driver);

        }
    }
}