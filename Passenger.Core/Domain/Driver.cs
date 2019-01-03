using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Passenger.Core.Domain
{
    public class Driver
    {
        public Guid UserId { get; protected set; }
        public string Name { get; protected set; }
        public Vehicle Vehicle { get; protected set; }
        public IEnumerable<Route> Routes { get; set; }
        public IEnumerable<DailyRoute> DailyRoutes { get; set; }
        public DateTime UpdatedAt { get; protected set; }

        protected Driver()
        {
        }
    
        public Driver(User user)
        {
            UserId = user.UserId;
            Name = user.Username;
        //    We can create static method that create vehicle objects
        //    Vehicle = Vehicle.Create();
        //
        }

        public void SetVehicle(Vehicle vehicle)
        {
            Vehicle = vehicle;
            UpdatedAt = DateTime.UtcNow;
        }

        //public void AddRoute(string name, Node start, Node end, double length)
        //{
        //    var route = Routes.SingleOrDefault(x => x.Name == name);
        //    if (route != null)
        //    {
        //        throw new Exception($"Route with name: '{name}' already exists for driver: {Name}.");
        //    }
        //    _routes.Add(Route.Create(name, start, end, length));
        //    UpdatedAt = DateTime.UtcNow;
        //}
    }
}
