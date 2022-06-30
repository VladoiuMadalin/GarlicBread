using GameStore.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.Exceptions;


namespace GameStore.DataLayer.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(GameStoreContext context) : base(context)
        {
        }

        public Product GetProductByTitle(string title)
        {
            Product result = GetRecords().Single(product => product.Title == title);
            return result;
        }

        public override void Insert(Product record)
        {
            var titleExists = _dbSet.Any(x => x.Title == record.Title);
            
            if (titleExists)
            {
                throw new TitleExistsException();
            }
            _dbSet.Add(record);
        }

    }
}

