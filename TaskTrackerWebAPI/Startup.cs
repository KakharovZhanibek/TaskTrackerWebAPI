using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TaskTrackerWebAPI.DAL.Context;
using TaskTrackerWebAPI.DAL.Entities;
using TaskTrackerWebAPI.DAL.Repositories.Implementation;
using TaskTrackerWebAPI.DAL.Repositories.Interfaces;
using TaskTrackerWebAPI.View_Models;

namespace TaskTrackerWebAPI
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

            //  services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));
            var connectionString = Configuration.GetValue<string>("SpecialConnectionStrings");

            services.AddDbContext<AppDbContext>(option => option.UseSqlServer(connectionString));

            services.AddControllers().AddNewtonsoftJson(options =>
                         options.SerializerSettings
                                .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            RegisterAutoMapper(services);
            RegisterRepositories(services);

            services.AddAutoMapper(typeof(Startup));
        }

        private void RegisterRepositories(IServiceCollection services)
        {
            IEnumerable<Type> implementationsType = Assembly
               .Load("TaskTrackerWebAPI.DAL")
               .GetTypes()
               .Where(type =>
                       !type.IsInterface && type.GetInterface(typeof(IBaseRepository<>).Name) != null);

            foreach (Type implementationType in implementationsType)
            {
                IEnumerable<Type> servicesType = implementationType
                    .GetInterfaces()
                    .Where(r => !r.Name.Contains(typeof(IBaseRepository<>).Name) && !r.Name.Contains(typeof(IDisposable).Name));

                foreach (Type serviceType in servicesType)
                {
                    services.AddScoped(serviceType, implementationType);
                }
            }
        }
        private void RegisterAutoMapper(IServiceCollection services)
        {
            var configurationExpression = new MapperConfigurationExpression();

            MapBothSide<ProjectEntity, ProjectVM>(configurationExpression);
            MapBothSide<TaskEntity, TaskVM>(configurationExpression);

            var config = new MapperConfiguration(configurationExpression);
            var mapper = new Mapper(config);

            services.AddScoped<IMapper>(x => mapper);
        }

        public void MapBothSide<Type1, Type2>(MapperConfigurationExpression configurationExpression)
        {
            configurationExpression.CreateMap<Type1, Type2>();
            configurationExpression.CreateMap<Type2, Type1>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseSwagger();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
