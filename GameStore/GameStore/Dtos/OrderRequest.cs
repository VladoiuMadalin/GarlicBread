using GameStore.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Dtos
{
    public class OrderRequest
    {
        public UserEntity User;
        public ICollection<ProductEntity> Products { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
