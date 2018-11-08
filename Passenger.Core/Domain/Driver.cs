using System;
using System.Collections.Generic;
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

        protected Driver()
        {
        }
    
        public Driver(User user)
        {
            UserId = user.Id;
            Name = user.Username;
        //    We can create static method that create vehicle objects
        //    Vehicle = Vehicle.Create();
        //
        }



    }
}
