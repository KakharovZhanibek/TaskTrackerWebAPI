using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTrackerWebAPI.DAL.Abstract;

namespace TaskTrackerWebAPI.DAL.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : BaseEntity
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        TEntity GetById(int id);
        List<TEntity> GetAll();
        bool DeleteById(int id);
        IQueryable<TEntity> GetAllAsIQueryable();
        void Save();
    }
}
