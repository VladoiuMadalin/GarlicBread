using GameStore.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Dtos
{
    public class ShoppingCartDto
    {
        public ICollection<ProductEntity> Products { get; set; }
        public UserEntity User { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
