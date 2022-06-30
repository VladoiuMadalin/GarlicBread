using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Dtos
{
    public class UserRequest : LightUserDto
    {
        public ICollection<OrderDto> Orders { get; set; }
    }
}
