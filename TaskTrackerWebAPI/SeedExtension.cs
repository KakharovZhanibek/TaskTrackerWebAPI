using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTrackerWebAPI.DAL.Entities;
using TaskTrackerWebAPI.DAL.Repositories.Interfaces;

namespace TaskTrackerWebAPI
{
    public static class SeedExtension
    {
        public static IHost SeedAsync(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                CreateDefaultProjects(scope.ServiceProvider);
            }
            return host;
        }
        public static void CreateDefaultProjects(IServiceProvider serviceProvider)
        {
            var projectRepository = serviceProvider.GetService<IProjectRepository>();
            var projects = projectRepository.GetAll().Any();

            if (!projects)
            {
                ProjectEntity project1 = new ProjectEntity()
                {
                    Name = "Project777",
                    StartDate = DateTime.Now,
                    CompletionDate = null,
                    CreatedDate = DateTime.Now,
                    Priority = 2,
                    Status = ProjectStatus.Active,
                    Tasks = new List<TaskEntity>()
                    {
                        new TaskEntity()
                        {
                            Name="Task1",
                            CreatedDate=DateTime.Now,
                            Description="desc1",
                            Priority=3,
                            Status= DAL.Entities.TaskStatus.InProgress
                        },
                        new TaskEntity()
                        {
                            Name="Task2",
                            CreatedDate=DateTime.Now,
                            Description="desc2",
                            Priority=4,
                            Status= DAL.Entities.TaskStatus.ToDo
                        }
                    }
                };

                ProjectEntity project2 = new ProjectEntity()
                {
                    Name = "Project9000",
                    StartDate = DateTime.Now.AddMonths(-3),
                    CompletionDate = null,
                    CreatedDate = DateTime.Now.AddMonths(-3),
                    Priority = 3,
                    Status = ProjectStatus.Active,
                    Tasks = new List<TaskEntity>()
                    {
                        new TaskEntity()
                        {
                            Name="Task1",
                            CreatedDate=DateTime.Now.AddMonths(-2),
                            Description="desc1",
                            Priority=2,
                            Status= DAL.Entities.TaskStatus.Done
                        },
                        new TaskEntity()
                        {
                            Name="Task2",
                            CreatedDate=DateTime.Now.AddMonths(-1),
                            Description="desc2",
                            Priority=4,
                            Status= DAL.Entities.TaskStatus.InProgress
                        }
                    }
                };

                projectRepository.Add(project1);
                projectRepository.Add(project2);
            }
        }
    }
}
