using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.DataLayer.Entities
{

    public class Product : BaseEntity
    {
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Picture { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<ShoppingCart> ShoppingCarts { get; set; }

    }
}
