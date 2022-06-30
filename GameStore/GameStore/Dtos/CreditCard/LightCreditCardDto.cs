using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Dtos
{
    public class LightCreditCardDto
    {
        [MaxLength(12)]
        [MinLength(12)]
        public string CardNumber { get; set; }

        public string NameOnCard { get; set; }
        public int ExpirationMonth { get; set; }
        public int ExpirationYear { get; set; }

        [MaxLength(3)]
        [MinLength(3)]
        public string Cvc { get; set; }
    }
}
