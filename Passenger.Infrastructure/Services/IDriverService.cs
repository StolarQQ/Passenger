﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Services
{
    public interface IDriverService : IService
    {
        Task<DriverDto> GetAsync(Guid userId);
        Task CreateAsync(Guid userid);
        Task SetVehicleAsync(Guid userid, string brand, string name, int seats);
        Task<IEnumerable<DriverDto>> BrowseAsync();
    }
}