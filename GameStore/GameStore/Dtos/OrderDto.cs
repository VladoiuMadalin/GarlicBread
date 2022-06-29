using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Dtos
{
    public class OrderDto
    {
        public IList<ProductDto> Products { get; set; }
    }
}
