using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroServices.Services.Activities.Domain.Models;

namespace MicroServices.Services.Activities.Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category> GetAsync(string name);
        Task<IQueryable<Category>> BrowseAsync();
        Task AddAsync(Category category);

    }
}
