using System;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services
{
    public class RouteManger : IRouteManger
    {
        // TODO Implement google maps 
        private static readonly Random Random = new Random();

        public Task<string> GetAddressAsync(double latitude, double longitude)
            => Task.FromResult($"Sample adress {Random.Next(100)}.");
       

        public double CalculateDistance(double startLatitude, double startLongitude, double endLatitude, double endLongitude)
            => Random.Next(500,1000);
    }
}