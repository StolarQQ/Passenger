﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Passenger.Core.Domain;

namespace Passenger.Core.Repositories
{
    public interface IDriverRepository : IRepository
    {
        Task<Driver> GetAsync(Guid userId);
        Task<IEnumerable<Driver>> BrowseAsync();
        Task AddAsync(Driver driver);
        Task UpdateAsync(Driver driver);
        Task DeleteAsync(Driver driver);
    }
}