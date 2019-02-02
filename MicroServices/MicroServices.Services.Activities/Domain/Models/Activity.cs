using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroServices.Services.Activities.Domain.Models
{
    public class Activity
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }

        public string Category { get; protected set; }
        public string Description { get; protected set; }
        public Guid UserId { get; protected set; }
        public DateTime CreatedAt { get; set; }

        protected Activity()
        {

        }

        public Activity(Guid id, string name, Category category, Guid userId, string description, DateTime createdAt)
        {
            Id = id;
            Name = name.ToLowerInvariant() ?? throw new ArgumentNullException(nameof(name));
            Category = category?.Name ?? throw new ArgumentNullException(nameof(category));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            CreatedAt = createdAt;
            UserId = userId;
        }
    }
}
