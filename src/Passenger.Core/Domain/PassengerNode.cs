﻿namespace Passenger.Core.Domain
{
    public class PassengerNode
    {
        // Value object

        public Node Node { get; protected set; }
        public Passenger Passenger { get; protected set; }

        // Constructor for serialization
        protected PassengerNode()
        {
            
        }

        public PassengerNode(Passenger passenger, Node node)
        {
            Passenger = passenger;
            Node = node;
        }

        public static PassengerNode Create(Passenger passenger, Node node)
            => new PassengerNode(passenger, node);
    }
}