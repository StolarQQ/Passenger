using System.Runtime.CompilerServices;
using AutoMapper.Configuration;
using Microsoft.Extensions.Configuration;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Passenger.Infrastructure.Extenstions
{
    public static class SettingsExtensions
    {
        public static T GetSettings<T>(this IConfiguration configuration) where T : new ()
        {
            
            var section = typeof(T).Name.Replace("Settings", string.Empty);
            var configurationValue = new T();
            // Assign settings from appsettings.json to configurationValue (Extensions.Binder lib) 
            configuration.GetSection(section).Bind(configurationValue);

            return configurationValue;
        }
    }
}