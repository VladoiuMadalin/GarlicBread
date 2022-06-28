using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.DataLayer.Entities
{
    public class ProductEntity : BaseEntity
    {
        public ICollection<OrderEntity> Orders { get; set; }
        public ICollection<ShoppingCartEntity> ShoppingCarts { get; set; }
    }
}
