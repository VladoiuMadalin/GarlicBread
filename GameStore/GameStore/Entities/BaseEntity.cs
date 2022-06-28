using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public BaseEntity()
        {
            //Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }
    }
}
