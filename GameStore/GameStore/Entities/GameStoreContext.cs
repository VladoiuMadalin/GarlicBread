using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Entities
{
    public class GameStoreContext :DbContext
    {
        public GameStoreContext(DbContextOptions<GameStoreContext> options) :base(options)
        {
        }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UserEntity> Orders { get; set; }
        public DbSet<UserEntity> Products { get; set; }
        public DbSet<UserEntity> ShoppingCarts{ get; set; }
        
    }
}
