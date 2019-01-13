using System;

namespace Passenger.Core.Domain
{
    public class Route
    {
        public string Name { get; protected set; }
        public Node Start { get; protected set; }
        public Node End { get; protected set; }
        public double Distance { get; set; }


        // Ctor for serialization
        protected Route()
        {
            
        }

        public Route(string name, Node start, Node end, double distance)
        {
            Name = name;
            Start = start;
            End = end;
            Distance = distance;
        }

        public static Route Create(string name, Node start, Node end, double distance)
            => new Route(name, start, end, distance);
    }

}
