using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.DataLayer.Entities
{
    public class CreditCardEntity : BaseEntity
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

       // [ForeignKey(nameof(UserEntity))]
        public Guid UserId { get; set; }
        public UserEntity User { get; set; }
    }
}
