using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTrackerWebAPI.DAL.Abstract;
using TaskTrackerWebAPI.DAL.Context;
using TaskTrackerWebAPI.DAL.Repositories.Interfaces;

namespace TaskTrackerWebAPI.DAL.Repositories.Implementation
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected AppDbContext _dbContext;
        protected DbSet<TEntity> _dbSet;

        public BaseRepository(AppDbContext context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            entity.CreatedDate = DateTime.Now;
            _dbSet.Add(entity);
        }

        public bool DeleteById(int id)
        {
            if (!_dbSet.Any(x => x.Id == id))
            {
                return false;
            }
            _dbSet.Remove(_dbSet.FirstOrDefault(x => x.Id == id));
            return true;
        }

        public IQueryable<TEntity> GetAllAsIQueryable()
        {
            return _dbSet.AsQueryable();
        }

        public TEntity GetById(int id)
        {
            return _dbSet.FirstOrDefault(x => x.Id == id);
        }

        public void Update(TEntity entity)
        {
            if (!_dbSet.Any(x => x.Id == entity.Id))
                entity.CreatedDate = DateTime.Now;
                  
            //_dbContext.Entry(entity).State = EntityState.Modified;
            //_dbContext.SaveChanges();
            _dbSet.Update(entity);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }


        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual List<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }
    }
}
