using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.Entities;

namespace GameStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var options = new DbContextOptionsBuilder<GameStoreContext>()
            //   .UseSqlServer(@"Server=DESKTOP-IIALOIB\MSSQLSERVER01;Database=GameStore;Trusted_Connection=True;")
            //   .Options;
            //using (var db = new GameStoreContext(options))
            //{
            //    db.Database.EnsureDeleted();
            //    db.Database.EnsureCreated();
            //}


            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
