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
    public class OrderRepository : RepositoryBase<OrderEntity>, IOrderRepository
    {
        public OrderRepository(GameStoreContext context) : base(context)
        {
        }

        public OrderEntity GetOrderByUser( UserEntity user)
        {
            OrderEntity result = GetRecords().FirstOrDefault(order =>order.User==user);
            return result;
        }

        public override void Insert(OrderEntity record)
        {
            var userOrderExists = _dbSet.Any(x => x.User == record.User);

            if (userOrderExists)
            {
                throw new OrderForUserExistsException();
            }

            if (_db.Entry(record).State == EntityState.Detached)
            {
                _db.Attach(record);
                _db.Entry(record).State = EntityState.Added;
            }


        }

    }
}

