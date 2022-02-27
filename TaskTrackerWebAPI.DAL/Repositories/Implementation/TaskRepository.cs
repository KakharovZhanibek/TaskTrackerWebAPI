using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskTrackerWebAPI.DAL.Abstract;
using TaskTrackerWebAPI.DAL.Context;
using TaskTrackerWebAPI.DAL.Entities;
using TaskTrackerWebAPI.DAL.Repositories.Interfaces;

namespace TaskTrackerWebAPI.DAL.Repositories.Implementation
{
    public class TaskRepository : BaseRepository<TaskEntity>, ITaskRepository
    {
        public TaskRepository(AppDbContext context)
            : base(context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<TaskEntity>();
        }
        
        public List<TaskEntity> GetAllTasksInProject(int projectId)
        {
            return GetAll().Where(t => t.Project.Id == projectId).ToList();
        }
    }
}
