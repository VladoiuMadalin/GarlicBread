using GameStore.DataLayer.Entities;
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

        public UnitOfWork(GameStoreContext context, IUserRepository users)
        {
            _dbContext = context;

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            Users = users;
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
