using GameStore.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.DataLayer.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User GetUserByUsername(string username);
        User GetAccount(Guid id);
    }
}
