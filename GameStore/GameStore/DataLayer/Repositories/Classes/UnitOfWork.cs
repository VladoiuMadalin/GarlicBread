using GameStore.DataLayer.Entities;
using GameStore.DataLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.DataLayer.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;

        public IUserRepository Users { get; set; }
        public IOrderRepository Orders { get; set; }
        public IShoppingCartRepository ShoppingCarts { get; set; }
        public IProductRepository Products{ get; set; }
        public ICreditCardRepository CreditCards { get; set; }

        public UnitOfWork(GameStoreContext context,
            IUserRepository users ,
            IProductRepository products,
            IOrderRepository orders ,
            IShoppingCartRepository  shoppingCarts,
            ICreditCardRepository  creditCards)
        {
            _dbContext = context;

           // context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();

            Users = users;
            Products = products;
            Orders = orders;
            ShoppingCarts = shoppingCarts;
            CreditCards = creditCards;
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                var savedChanges = await _dbContext.SaveChangesAsync();
                return savedChanges > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
