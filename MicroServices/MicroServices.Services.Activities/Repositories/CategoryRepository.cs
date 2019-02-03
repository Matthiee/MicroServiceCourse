using System;
using System.Linq;
using System.Threading.Tasks;
using MicroServices.Services.Activities.Domain.Models;
using MicroServices.Services.Activities.Domain.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace MicroServices.Services.Activities.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMongoDatabase database;

        public CategoryRepository(IMongoDatabase database)
        {
            this.database = database ?? throw new ArgumentNullException(nameof(database));
        }

        public async Task AddAsync(Category category)
            => await Collection.InsertOneAsync(category);

        public IMongoQueryable<Category> Browse()
            => Collection.AsQueryable();

        public async Task<Category> GetAsync(string name)
            => await Browse().FirstOrDefaultAsync(cat => cat.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));

        private IMongoCollection<Category> Collection => database.GetCollection<Category>("Categories");
    }
}
