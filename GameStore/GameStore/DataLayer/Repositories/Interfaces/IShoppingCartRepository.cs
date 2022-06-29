using GameStore.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.DataLayer.Repositories.Interfaces
{
    public interface IShoppingCartRepository : IRepositoryBase<ShoppingCartEntity>
    {
        ShoppingCartEntity GetShoppingCartByUser(UserEntity user);
    }
}
