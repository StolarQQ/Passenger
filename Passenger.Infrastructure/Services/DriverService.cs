using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Services
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public DriverService(IDriverRepository driverRepository, IUserRepository userRepository, IMapper mapper)
        {
            _driverRepository = driverRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<DriverDto> GetAsync(Guid userId)
        {
            var driver = await _driverRepository.GetAsync(userId);

            return _mapper.Map<Driver, DriverDto>(driver);
        }

        public async Task<IEnumerable<DriverDto>> BrowseAsync()
        {
            var drivers = await _driverRepository.BrowseAsync();
            if (drivers == null)
            {
                throw new Exception("Drivers not exist");
            }

            return _mapper.Map<IEnumerable<Driver>, IEnumerable<DriverDto>>(drivers);
        }


        public async Task CreateAsync(Guid userid)
        {
            var user = await _userRepository.GetAsync(userid);

            if (user == null)
            {
                throw new Exception($"User with id '{userid}' not found");
            }

            var driver = await _driverRepository.GetAsync(userid);
            if (driver != null)
            {
                throw new Exception($"Driver with id '{userid}' already exist");
            }

            driver = new Driver(user);
            await _driverRepository.AddAsync(driver);

        }

        public async Task SetVehicleAsync(Guid userid, string brand, string name, int seats)
        {
            var driver = await _driverRepository.GetAsync(userid);
            if (driver == null)
            {
                throw new Exception($"Driver with id {userid} was not found");
            }

            driver.SetVehicle(brand, name, seats);
            await _driverRepository.UpdateAsync(driver);
        }
    }
}