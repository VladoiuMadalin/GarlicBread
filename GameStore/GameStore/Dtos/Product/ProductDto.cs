using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Dtos
{
    public class ProductDto : LightProductDto   
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public string Picture { get; set; }
    }
}
