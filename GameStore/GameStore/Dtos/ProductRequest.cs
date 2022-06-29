using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Dtos
{
    public class ProductRequest:LightProductRequest
    {
        


        [Required(ErrorMessage = "Price required!")]
        public decimal Price { get; set; }


       
    }
}
