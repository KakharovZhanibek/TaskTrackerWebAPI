using System;
using System.Collections.Generic;
using System.Text;
using TaskTrackerWebAPI.DAL.Entities;

namespace TaskTrackerWebAPI.DAL.Repositories.Interfaces
{
    public interface IProjectRepository : IBaseRepository<ProjectEntity>
    {
    }
}
