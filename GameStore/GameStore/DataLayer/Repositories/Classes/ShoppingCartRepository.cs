using GameStore.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.Exceptions;

namespace GameStore.DataLayer.Repositories
{
    public class ShoppingCartRepository : BaseRepository<ShoppingCart>, IShoppingCartRepository
    {
        public ShoppingCartRepository(GameStoreContext context) : base(context)
        {
        }

        public ShoppingCart GetShoppingCartByUser(User user)
        {
            ShoppingCart result = GetRecords().FirstOrDefault(shoppingCart=>shoppingCart.User==user);
            return result;
        }

        public override void Insert(ShoppingCart record)
        {
            _dbSet.Add(record);
        }

    }
}

