using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.DataLayer.Repositories
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        //INotificationsRepository Notifications { get; set; }

        Task<bool> SaveChangesAsync();
    }
}
