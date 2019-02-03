using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroServices.Common.Commands;
using MicroServices.Common.Events;
using MicroServices.Common.Mongo;
using MicroServices.Common.RabbitMq;
using MicroServices.Services.Activities.Domain.Repositories;
using MicroServices.Services.Activities.Handlers;
using MicroServices.Services.Activities.Repositories;
using MicroServices.Services.Activities.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MicroServices.Services.Activities
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddMongoDb(Configuration);
            services.AddRabbitMq(Configuration);
            services.AddTransient<ICommandHandler<CreateActivity>, CreateActivityHandler>();

            services.AddTransient<IActivityRepository, ActivityRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();

            services.AddTransient<IDatabaseSeeder, ActivitiesMongoSeeder>();

            services.AddTransient<IActivityService, ActivityService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.ApplicationServices.GetService<IDatabaseInitializer>().InitialzeAsync();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
