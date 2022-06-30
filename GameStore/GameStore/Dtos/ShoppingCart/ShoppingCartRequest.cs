using GameStore.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Dtos
{
    public class ShoppingCartRequest
    {
        public ICollection<Product> Products { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
