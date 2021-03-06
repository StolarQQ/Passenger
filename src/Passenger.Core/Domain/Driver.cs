﻿using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace Passenger.Core.Domain
{
    public class Driver
    { 
        // Added element for mapping in Mongodb 
        [BsonElement]
        private ISet<Route> _routes = new HashSet<Route>();
        private ISet<DailyRoute> _dailyRoutes = new HashSet<DailyRoute>();

        public Guid UserId { get; protected set; }
        public string Name { get; protected set; }
        public Vehicle Vehicle { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        public IEnumerable<Route> Routes => _routes;
        public IEnumerable<DailyRoute> DailyRoutes => _dailyRoutes;

        
        protected Driver()
        {
        }
    
        public Driver(User user)
        {
            UserId = user.UserId;
            Name = user.Username;
        }

        public Driver SetVehicle(Vehicle vehicle)
        {
            Vehicle = vehicle;
            UpdatedAt = DateTime.UtcNow;

            return this;
        }

        public void AddRoute(string name, Node start, Node end, double distance)
        {
            var route = Routes.SingleOrDefault(x => x.Name == name);
            if (route != null)
            {
                throw new Exception($"Route with name: '{name}' already exists for driver: {Name}.");
            }

            if (distance < 0)
            {
                throw new Exception($"Route with name '{name}' can not have negative distance");
            }
            _routes.Add(Route.Create(name, start, end, distance));
            
            UpdatedAt = DateTime.UtcNow;
        }

        public void DeleteRoute(string name)
        {
            var route = Routes.SingleOrDefault(x => x.Name == name);
            if (route == null)
            {
                throw new Exception($"Route with name: '{name}' for driver'{Name}' was not found.");
            }

            _routes.Remove(route);
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
