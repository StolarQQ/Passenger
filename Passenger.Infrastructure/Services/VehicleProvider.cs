using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Services
{
    public class VehicleProvider : IVehicleProvider
    {
        private readonly IMemoryCache _cache;
        private static readonly string CacheKey = "vehicles";

        private static readonly IDictionary<string, IEnumerable<VehicleDetails>> avaliableVehicles =
            new Dictionary<string, IEnumerable<VehicleDetails>>
            {
                ["Audi"] = new List<VehicleDetails>
                {
                    new VehicleDetails("RS8", 5)
                },
                ["BMW"] = new List<VehicleDetails>
                {
                    new VehicleDetails("i8", 3),
                    new VehicleDetails("E35", 5)
                },
                ["Ford"] = new List<VehicleDetails>
                {
                    new VehicleDetails("Fiesta", 3)
                },
                ["Skoda"] = new List<VehicleDetails>
                {
                    new VehicleDetails("Fabia", 5),
                    new VehicleDetails("Rapid", 5),
                },
                ["VW"] = new List<VehicleDetails>
                {
                    new VehicleDetails("Passat", 5)
                },
                ["Citroen"] = new List<VehicleDetails>
                {
                    new VehicleDetails("C5", 5)
                },
            };

        public VehicleProvider(IMemoryCache cache)
        {
            _cache = cache;
        }

        /// <summary>
        /// Cached vehicles dictionary
        /// </summary>
        /// <returns>Cached data</returns>
        public async Task<IEnumerable<VehicleDto>> BrowseAsync()
        {
            // Check if vehicles is already cached
            var vehicles = _cache.Get<IEnumerable<VehicleDto>>(CacheKey);
            
            if (vehicles == null) // !vehicles.Any()
            {
                vehicles = await GetAllAsync();
                _cache.Set(CacheKey, vehicles);
            }

            return vehicles;
        }

        public async Task<VehicleDto> GetAsync(string brand, string name)
        {
            if (!avaliableVehicles.ContainsKey(brand))
            {
                throw new Exception($"Vehicle brand : '{brand}' is no available ");
            }
            var vehicles = avaliableVehicles[brand];
            var vehicle = vehicles.SingleOrDefault(x => x.Name == name);
            if (vehicle == null)
            {
                throw new Exception($"Vehicles: '{name}' for brand '{brand}' is not available");
            }

            return await Task.FromResult(new VehicleDto
            {
                Brand = brand,
                Name = vehicle.Name,
                Seats = vehicle.Seats
            });
        }
        
        public async Task<IEnumerable<VehicleDto>> GetAllAsync()
            => await Task.FromResult(avaliableVehicles.GroupBy(x => x.Key)
                .SelectMany(g => g.SelectMany(v => v.Value.Select(c => new VehicleDto
                {
                    Brand = v.Key,
                    Name = c.Name,
                    Seats = c.Seats

                }))));
        
        private class VehicleDetails
        {
            public int Seats { get; }
            public string Name { get; }

            public VehicleDetails(string name, int seats)
            {
                Name = name;
                Seats = seats;
            }
        }
    }
}