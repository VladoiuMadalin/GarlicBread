using GameStore.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.Exceptions;


namespace GameStore.DataLayer.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(GameStoreContext context) : base(context)
        {
        }

        public Order GetOrderByUser( User user)
        {
            Order result = GetRecords().FirstOrDefault(order =>order.User==user);
            return result;
        }

        public override void Insert(Order record)
        {
            _dbSet.Add(record);
        }

    }
}

