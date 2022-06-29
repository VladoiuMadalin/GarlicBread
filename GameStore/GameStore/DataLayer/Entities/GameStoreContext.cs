using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.DataLayer.Entities
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.Development.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("MadaliConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
