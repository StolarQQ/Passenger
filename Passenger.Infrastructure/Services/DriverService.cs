using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.DTO;
using Passenger.Infrastructure.Exceptions;
using Passenger.Infrastructure.Extenstions;
using ErrorCodes = Passenger.Infrastructure.Exceptions.ErrorCodes;

namespace Passenger.Infrastructure.Services
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IVehicleProvider _vehicleProvider;

        public DriverService(IDriverRepository driverRepository, IUserRepository userRepository, IMapper mapper, IVehicleProvider vehicleProvider)
        {
            _driverRepository = driverRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _vehicleProvider = vehicleProvider;
        }

        public async Task<DriverDetailsDto> GetAsync(Guid userId)
        {
            var driver = await _driverRepository.GetAsync(userId);

            return _mapper.Map<Driver, DriverDetailsDto>(driver);
        }

        public async Task<IEnumerable<DriverDto>> BrowseAsync()
        {
            var drivers = await _driverRepository.BrowseAsync();
            if (drivers == null)
            {
                throw new ServiceException(ErrorCodes.DriverNotFound, "Drivers not exist");
            }

            return _mapper.Map<IEnumerable<Driver>, IEnumerable<DriverDto>>(drivers);
        }
        
        public async Task CreateAsync(Guid userId)
        {
            var user = await _userRepository.GetOrFailAsync(userId);
            var driver = await _driverRepository.GetAsync(userId);
            if (driver != null)
            {
                throw new ServiceException(ErrorCodes.DriverAlreadyExist, $"Driver with id '{userId}' already exist");
            }

            driver = new Driver(user);
            await _driverRepository.AddAsync(driver);
        }

        public async Task SetVehicleAsync(Guid userId, string brand, string name)
        {
            var driver = await _driverRepository.GetOrFailAsync(userId);
            var vehicledetials = await _vehicleProvider.GetAsync(brand, name);
            var vehicle = Vehicle.Create(vehicledetials.Brand, vehicledetials.Name, vehicledetials.Seats);
            driver.SetVehicle(vehicle);
        }

        public async Task DeleteAsync(Guid userId)
        {
            var driver = await _driverRepository.GetOrFailAsync(userId);

            await _driverRepository.DeleteAsync(driver);
        }
    }
}