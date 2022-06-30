using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.DataLayer.Entities
{
    
    public class Order : BaseEntity
    {
        public ICollection<Product> Products { get; set; }
        public User User { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
