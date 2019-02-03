using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroServices.Services.Activities.Domain.Models;
using MongoDB.Driver.Linq;

namespace MicroServices.Services.Activities.Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category> GetAsync(string name);
        IMongoQueryable<Category> Browse();
        Task AddAsync(Category category);

    }
}
