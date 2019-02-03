using System;
using System.Linq;
using System.Threading.Tasks;
using MicroServices.Services.Activities.Domain.Models;
using MicroServices.Services.Activities.Domain.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace MicroServices.Services.Activities.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly IMongoDatabase database;

        public ActivityRepository(IMongoDatabase database)
        {
            this.database = database ?? throw new ArgumentNullException(nameof(database));
        }

        public async Task AddAsync(Activity activity)
            => await Collection.InsertOneAsync(activity);

        public async Task<Activity> GetAsync(Guid id)
            => await Collection.AsQueryable().FirstOrDefaultAsync(act => act.Id == id);

        private IMongoCollection<Activity> Collection => database.GetCollection<Activity>("Activities");
    }
}
