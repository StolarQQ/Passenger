using Autofac;
using AutoMapper.Configuration;
using Passenger.Infrastructure.IoC.Modules;
using Passenger.Infrastructure.Mapper;
using Passenger.Infrastructure.Settings;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Passenger.Infrastructure.IoC
{
    public class ContainerModule : Autofac.Module
    {
        private readonly IConfiguration _configuration;

        public ContainerModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(AutoMapperConfig.Initialize())
                .SingleInstance();
            builder.RegisterModule<RepositoryModule>();
            builder.RegisterModule<ServiceModule>();
            builder.RegisterModule<CommandModule>();
            builder.RegisterModule(new SettingsModule(_configuration));
        }
    }
}