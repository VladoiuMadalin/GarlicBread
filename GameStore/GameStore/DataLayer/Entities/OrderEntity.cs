using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.DataLayer.Entities
{
    public class OrderEntity : BaseEntity
    {
        public ICollection<ProductEntity> Products { get; set; }
        public UserEntity User { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
