using GameStore.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.DataLayer.Repositories.Interfaces
{
    public interface IProductRepository: IRepositoryBase<ProductEntity>
    {
        ProductEntity GetProductByTitle(string name);
    }
}
