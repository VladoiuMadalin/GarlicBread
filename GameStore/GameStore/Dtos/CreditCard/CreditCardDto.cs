using GameStore.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Dtos
{
    public class CreditCardDto : LightCreditCardDto
    {
        public LightUserDto User { get; set; }
    }
}
