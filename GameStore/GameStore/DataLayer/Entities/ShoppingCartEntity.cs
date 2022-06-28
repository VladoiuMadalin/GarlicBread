using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.DataLayer.Entities
{
    public class ShoppingCartEntity : BaseEntity
    {
       public ICollection<ProductEntity> Products { get; set; }
    }

    
}
