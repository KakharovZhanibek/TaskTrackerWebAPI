using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TaskTrackerWebAPI.DAL.Entities;

namespace TaskTrackerWebAPI.DAL.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<ProjectEntity> Projects { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
            => optionsBuilder
                .UseLazyLoadingProxies();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectEntity>()
                .HasMany(x => x.Tasks)
                .WithOne(x => x.Project)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProjectEntity>().HasData(
                    new ProjectEntity()
                    {
                        Id = 1,
                        Name = "Project777",
                        StartDate = DateTime.Now,
                        CompletionDate = null,
                        CreatedDate = DateTime.Now,
                        Priority = 2,
                        Status = ProjectStatus.Active
                    },
                    new ProjectEntity()
                    {
                        Id = 2,
                        Name = "Project9000",
                        StartDate = DateTime.Now.AddMonths(-3),
                        CompletionDate = null,
                        CreatedDate = DateTime.Now.AddMonths(-3),
                        Priority = 3,
                        Status = ProjectStatus.Active
                    }
                );

            modelBuilder.Entity<TaskEntity>().HasData(
                    new
                    {
                        Id = 1,
                        Name = "Task1",
                        CreatedDate = DateTime.Now,
                        Description = "desc1",
                        Priority = 3,
                        Status = DAL.Entities.TaskStatus.InProgress,
                        ProjectId = 1
                    },
                    new
                    {
                        Id = 2,
                        Name = "Task2",
                        CreatedDate = DateTime.Now,
                        Description = "desc2",
                        Priority = 4,
                        Status = DAL.Entities.TaskStatus.ToDo,
                        ProjectId = 1
                    },
                    new
                    {
                        Id = 3,
                        Name = "Task1",
                        CreatedDate = DateTime.Now.AddMonths(-2),
                        Description = "desc1",
                        Priority = 2,
                        Status = DAL.Entities.TaskStatus.Done,
                        ProjectId = 2
                    },
                    new
                    {
                        Id = 4,
                        Name = "Task2",
                        CreatedDate = DateTime.Now.AddMonths(-1),
                        Description = "desc2",
                        Priority = 4,
                        Status = DAL.Entities.TaskStatus.InProgress,
                        ProjectId = 2
                    }
                );
        }
    }
}
