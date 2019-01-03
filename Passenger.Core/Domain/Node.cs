using System;

namespace Passenger.Core.Domain
{
    public class Node
    {
        public string Address { get; protected set; }
        public double Longitude { get; protected set; }
        public double Latitude { get; protected set; }
        public DateTime UpdateAt { get; set; }

        public Node(string address, double longitude, double latitude)
        {
            Address = address;
            Longitude = longitude;
            Latitude = latitude;
            UpdateAt = DateTime.UtcNow;
        }

        protected Node()
        {

        }

        // TODO Validation 

        public static Node Create(string address, double longitude, double latitude)
            => new Node(address, longitude, latitude);

    }
}