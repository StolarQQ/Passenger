using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Conventions;

namespace Passenger.Infrastructure.Mongo
{
    public static class MongoConfiguration
    {
        private static bool _initalized;

        public static void Initialize()
        {
            if (_initalized)
            {
                return;
            }

            RegisterConvetions();
        }

        private static void RegisterConvetions()
        {
            ConventionRegistry.Register("PassengerConventions", new MongoConvetion(), x => true);
            _initalized = true;
        }

        private class MongoConvetion : IConventionPack
        {
            public IEnumerable<IConvention> Conventions => new List<IConvention>
            {
                new IgnoreExtraElementsConvention(true),
                new EnumRepresentationConvention(BsonType.String),
                new CamelCaseElementNameConvention()
            };
        }
    }
}