using System;
using System.Threading.Tasks;
using MicroServices.Services.Identity.Domain.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace MicroServices.Services.Identity.Domain.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoDatabase database;

        public UserRepository(IMongoDatabase database)
        {
            this.database = database ?? throw new ArgumentNullException(nameof(database));
        }

        public async Task AddUser(User user)
        => await Collection.InsertOneAsync(user);

        public async Task<User> GetAsync(Guid id)
        => await Collection.AsQueryable().FirstOrDefaultAsync(u => u.Id == id);

        public async Task<User> GetAsync(string email)
            => await Collection.AsQueryable().FirstOrDefaultAsync(u => u.Email == email);

        private IMongoCollection<User> Collection => database.GetCollection<User>("Users");
    }
}
