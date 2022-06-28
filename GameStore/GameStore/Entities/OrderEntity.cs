using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Entities
{
    public class OrderEntity : BaseEntity
    {
        public ICollection<ProductEntity> Products { get; set; }
    }
}
