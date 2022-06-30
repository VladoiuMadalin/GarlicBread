using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Dtos
{
    public class LightProductDto
    {
        [Required(ErrorMessage = "Title required!")]
        [MaxLength(100, ErrorMessage = "Title too long!")]
        public string Title { get; set; }
    }
}
