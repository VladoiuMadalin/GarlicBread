using GameStore.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.Exceptions;

namespace GameStore.DataLayer.Repositories
{
    public class UserRepository : RepositoryBase<UserEntity>, IUserRepository
    {
        public UserRepository(GameStoreContext context) : base(context)
        {
        }

        public UserEntity GetUserByUsername(string username)
        {
            var result = GetRecords().FirstOrDefault(user => user.Username == username);
            return result;
        }

        public override void Insert(UserEntity record)
        {
            var usernameExists = _dbSet.Any(x => x.Username == record.Username);
            var emailExists = _dbSet.Any(x => x.Email == record.Email);

            if (usernameExists)
            {
                throw new UserExistsException();
            }
            if (emailExists)
            {
                throw new EmailExistsException();
            }

            if (_db.Entry(record).State == EntityState.Detached)
            {
                _db.Attach(record);
                _db.Entry(record).State = EntityState.Added;
            }
        }
    }
}
