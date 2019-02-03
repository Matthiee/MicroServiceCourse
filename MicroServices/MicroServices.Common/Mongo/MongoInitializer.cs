using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace MicroServices.Common.Mongo
{
    public class MongoInitializer : IDatabaseInitializer
    {
        private bool initialized;
        private readonly IMongoDatabase database;
        private readonly IDatabaseSeeder seeder;
        private readonly bool seed;

        public MongoInitializer(IMongoDatabase database, IDatabaseSeeder seeder, IOptions<MongoOptions> options)
        {
            this.database = database ?? throw new ArgumentNullException(nameof(database));
            this.seeder = seeder;
            this.seed = options.Value.Seed;
        }

        public async Task InitialzeAsync()
        {
            if (initialized) return;

            RegisterConventions();
            initialized = true;

            if (!seed) return;

            await seeder.SeedAsync();
        }

        private void RegisterConventions()
        {
            ConventionRegistry.Register("MicroServicesConventions", new MongoConvention(), x => true);
        }

        private class MongoConvention : IConventionPack
        {
            public IEnumerable<IConvention> Conventions => new List<IConvention>
            {
                new IgnoreExtraElementsConvention(true),
                new EnumRepresentationConvention(MongoDB.Bson.BsonType.String),
                new CamelCaseElementNameConvention()
            };
        }
    }
}
