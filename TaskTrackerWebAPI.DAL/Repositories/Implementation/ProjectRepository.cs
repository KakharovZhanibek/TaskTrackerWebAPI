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
    public class ProjectRepository : BaseRepository<ProjectEntity>, IProjectRepository
    {
        public ProjectRepository(AppDbContext context)
            : base(context)
        {
            _dbContext = context;
        }

        public override List<ProjectEntity> GetAll()
        {
            return _dbSet.Include(p => p.Tasks).ToList();
        }
    }
}
