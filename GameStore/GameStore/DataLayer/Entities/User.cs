using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GameStore.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.DataLayer.Entities
{
    public class User : BaseEntity
    {
        [MaxLength(30)]
        public string Username { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public Role Role { get; set; }

        public ICollection<Order> Orders { get; set; }
        public ICollection<ShoppingCart> ShoppingCarts { get; set; }

        public CreditCard CreditCard { get; set; }
    }
}
