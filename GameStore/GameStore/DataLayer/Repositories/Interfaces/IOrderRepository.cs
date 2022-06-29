using GameStore.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.DataLayer.Repositories.Interfaces
{
    public interface IOrderRepository : IRepositoryBase<OrderEntity>
    {
        OrderEntity GetOrderByUser(UserEntity user);
    }
}
