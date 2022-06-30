using GameStore.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.Exceptions;

namespace GameStore.DataLayer.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(GameStoreContext context) : base(context)
        {
        }

        public User GetAccount(Guid id)
        {
            var result = GetRecords().Include(user => user.Orders).ThenInclude(order => order.Products)
                .Single(user => user.Id == id);

            return result;
        }

        public override void Delete(User record)
        {
            DeleteById(record.Id);
        }

        public override void DeleteById(Guid id)
        {
            var record = GetRecords().Include(u => u.Orders).Include(u => u.ShoppingCarts).Single(e => e.Id == id);
            OrderRepository orderRepository = new OrderRepository(_db as GameStoreContext);
            ShoppingCartRepository shoppingCartRepository = new ShoppingCartRepository(_db as GameStoreContext);

            base.Delete(record);
            foreach (var order in record.Orders)
            {
                orderRepository.Delete(order);
            }
            foreach(var cart in record.ShoppingCarts)
            {
                shoppingCartRepository.Delete(cart);
            }
        }

        public User GetUserByUsername(string username)
        {
            var result = GetRecords().Single(user => user.Username == username);
            return result;
        }

        public override void Insert(User record)
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
