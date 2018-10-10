using System;
using System.Collections.Generic;
using Passenger.Core.Domain;

namespace Passenger.Core.Repositories
{
    class IMemoryDriverRepository : IDriverRepository
    {
        public Driver Get(Guid userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Driver> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Add(Driver driver)
        {
            throw new NotImplementedException();
        }

        public void Update(Driver driver)
        {
            throw new NotImplementedException();
        }

        public void Delete(Driver driver)
        {
            throw new NotImplementedException();
        }
    }
}