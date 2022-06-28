using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Entities
{
    public class ShoppingCartEntity : BaseEntity
    {
        ICollection<ProductEntity> Products { get; set; }
    }
}
