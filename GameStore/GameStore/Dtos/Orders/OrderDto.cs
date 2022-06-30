using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Dtos
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public IList<ProductDto> Products { get; set; }
        public decimal TotalPrice { get; set; }
        public LightUserDto User { get; set; }
    }
}
