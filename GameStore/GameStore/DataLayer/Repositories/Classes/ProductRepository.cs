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
    public class ProductRepository : RepositoryBase<ProductEntity>, IProductRepository
    {
        public ProductRepository(GameStoreContext context) : base(context)
        {
        }

        public ProductEntity GetProductByTitle(string title)
        {
            ProductEntity result = GetRecords().Single(product => product.Title == title);
            return result;
        }

        public override void Insert(ProductEntity record)
        {
            var titleExists = _dbSet.Any(x => x.Title == record.Title);
            
            if (titleExists)
            {
                throw new TitleExistsException();
            }

            if (_db.Entry(record).State == EntityState.Detached)
            {
                _db.Attach(record);
                _db.Entry(record).State = EntityState.Added;
            }

           
        }

    }
}

