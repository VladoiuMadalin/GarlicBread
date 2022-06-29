using GameStore.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.DataLayer.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T>, IDisposable where T : BaseEntity, new()
    {
        protected readonly DbContext _db;
        protected readonly DbSet<T> _dbSet;

        protected RepositoryBase(GameStoreContext context)
        {
            _db = context;
            _dbSet = context.Set<T>();
        }

        public void Delete(T record)
        {
            if (record != null)
            {
                record.DeletedAt = DateTime.UtcNow;
                Update(record);
            }
        }

        public void DeleteById (Guid id)
        {
            var record = GetRecords().Single(e => e.Id == id);
            Delete(record);
        }

        public void Dispose()
        {
            _db?.Dispose();
        }

        public IList<T> GetAll(bool asNoTracking = false, bool includeDeleted = false)
        {
            var query = includeDeleted
               ? _dbSet
               : _dbSet.Where(entity => entity.DeletedAt == null);

            return asNoTracking
                ? query.AsNoTracking().ToList()
                : query.ToList();
        }

        public T GetById(Guid id, bool asNoTracking = false)
        {
            return GetRecords(asNoTracking).FirstOrDefault(e => e.Id == id);
        }

        public virtual void Insert(T record)
        {
            if (_db.Entry(record).State == EntityState.Detached)
            {
                _db.Attach(record);
                _db.Entry(record).State = EntityState.Added;
            }
        }

        public void Update(T record)
        {
            if (_db.Entry(record).State == EntityState.Detached)
                _db.Attach(record);

            _db.Entry(record).State = EntityState.Modified;
        }

        protected IQueryable<T> GetRecords(bool asNoTracking = false, bool includeDeleted = false)
        {
            var result = includeDeleted ? _dbSet.Where(e => e.DeletedAt != null) : _dbSet;
            return asNoTracking ? result.AsNoTracking() : result;
        }
    }
}