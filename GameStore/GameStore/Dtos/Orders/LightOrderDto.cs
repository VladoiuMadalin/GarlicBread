using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Dtos
{
    public class LightOrderDto
    {
        public IList<LightProductDto> Products { get; set; }
    }
}
