﻿using System;

namespace Passenger.Core.Domain
{
    public class Route
    {
        public string Name { get; protected set; }
        public Node Start { get; protected set; }
        public Node End { get; protected set; }

        // Ctor for serialization
        protected Route()
        {
            
        }

        public Route(string name, Node start, Node end)
        {
            Name = name;
            Start = start;
            End = end;
        }

        public static Route Create(string name, Node start, Node end)
            => new Route(name, start, end);
    }

}
