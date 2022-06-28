using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.DataLayer.Repositories
{
    public interface IRepositoryBase<T>
    {
        IList<T> GetAll(bool asNoTracking = false, bool includeDeleted = false);
        T GetById(Guid id, bool asNoTracking = false);
        void Insert(T record);
        void Update(T record);
        void Delete(T record);
        void DeleteById(Guid id);
    }
}
