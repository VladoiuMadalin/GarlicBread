using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GameStore.Enums;

namespace GameStore.DataLayer.Entities
{
    public class UserEntity : BaseEntity
    {
        [MaxLength(30)]
        // [Index(IsUnique = true)]
        public string Username { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public Role Role { get; set; }

        public ICollection<OrderEntity> Orders { get; set; }

    }
}
