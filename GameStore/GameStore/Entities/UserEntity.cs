using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Entities
{
    public class UserEntity : BaseEntity
    {
        [MaxLength(30)]
        public string Username { get; set; }

        [MaxLength(80)]
        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public ICollection<OrderEntity> Orders { get; set; }
    }
}
