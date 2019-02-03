using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroServices.Common.Mongo;
using MicroServices.Services.Activities.Domain.Models;
using MicroServices.Services.Activities.Domain.Repositories;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace MicroServices.Services.Activities.Services
{
    public class ActivitiesMongoSeeder : MongoSeeder
    {
        private readonly ICategoryRepository categoryRepository;

        public ActivitiesMongoSeeder(IMongoDatabase database, ICategoryRepository categoryRepository)
            : base(database)
        {
            this.categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        protected override async Task CustomSeedAsync()
        {
            var cats = new List<string>
            {
                "work",
                "sport",
                "hobby"
            };

            await Task.WhenAll(cats.Select(c => categoryRepository.AddAsync(new Category(c))));

            Console.WriteLine("Seeded");
        }
    }
}
