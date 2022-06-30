using GameStore.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Dtos
{
    public class ShoppingCartDto
    {
        public IList<ProductDto> Products { get; set; }
        public decimal TotalPrice { get; set; }
        public LightUserDto User { get; set; }
    }
}
