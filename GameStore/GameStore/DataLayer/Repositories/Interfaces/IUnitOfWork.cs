
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.DataLayer.Repositories
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IProductRepository Products { get; }
        IShoppingCartRepository ShoppingCarts { get; }
        IOrderRepository Orders { get; }
        ICreditCardRepository CreditCards { get; }
        //INotificationsRepository Notifications { get; set; }

        Task<bool> SaveChangesAsync();
    }
}
