using GameStore.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.DataLayer.Repositories
{
    public interface IShoppingCartRepository : IBaseRepository<ShoppingCart>
    {
        ShoppingCart GetShoppingCartByUser(User user);
    }
}
