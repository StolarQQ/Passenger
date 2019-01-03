using System;
using AutoMapper;
using Passenger.Core.Domain;
using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Mapper
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<User, UserDto>();
                    cfg.CreateMap<Driver, DriverDto>();
                    cfg.CreateMap<Driver, DriverDetailsDto>();
                    cfg.CreateMap<Route, RouteDto>();
                    cfg.CreateMap<Node, NodeDto>();
                    cfg.CreateMap<Vehicle, VehicleDto>();
                })
                .CreateMapper(); // Create Interface with our configuration
        
    }
}