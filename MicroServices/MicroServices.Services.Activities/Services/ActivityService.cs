using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroServices.Common.Exceptions;
using MicroServices.Services.Activities.Domain.Models;
using MicroServices.Services.Activities.Domain.Repositories;

namespace MicroServices.Services.Activities.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository activityRepository;
        private readonly ICategoryRepository categoryRepository;

        public ActivityService(IActivityRepository activityRepository, ICategoryRepository categoryRepository)
        {
            this.activityRepository = activityRepository ?? throw new ArgumentNullException(nameof(activityRepository));
            this.categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        public async Task AddAsync(Guid id, Guid userId, string category, string name, string description, DateTime createdAt)
        {
            var activityCat = await categoryRepository.GetAsync(category);

            if (activityCat == null) throw new MicroServiceException("category_not_found", $"Category '{category}' not found.");

            var activity = new Activity(id, name, activityCat, userId, description, createdAt);

            await activityRepository.AddAsync(activity);

        }
    }
}
