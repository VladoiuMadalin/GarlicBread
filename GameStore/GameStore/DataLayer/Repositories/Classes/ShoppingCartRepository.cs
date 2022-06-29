using GameStore.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.Exceptions;
using GameStore.DataLayer.Repositories.Interfaces;

namespace GameStore.DataLayer.Repositories.Classes
{
    public class ShoppingCartRepository : RepositoryBase<ShoppingCartEntity>, IShoppingCartRepository
    {
        public ShoppingCartRepository(GameStoreContext context) : base(context)
        {
        }

        public ShoppingCartEntity GetShoppingCartByUser(UserEntity user)
        {
            ShoppingCartEntity result = GetRecords().FirstOrDefault(shoppingCart=>shoppingCart.User==user);
            return result;
        }

        public override void Insert(ShoppingCartEntity record)
        {
            var userShoppingCartExists = _dbSet.Any(x => x.User == record.User);

            if (userShoppingCartExists)
            {
                throw new ShoppingCartForUserExistsException();
            }

            if (_db.Entry(record).State == EntityState.Detached)
            {
                _db.Attach(record);
                _db.Entry(record).State = EntityState.Added;
            }


        }

    }
}

