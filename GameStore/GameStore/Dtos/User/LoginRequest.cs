using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Dtos
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Username required!")]
        [MaxLength(30, ErrorMessage = "Username too long!")]
        public string Username { get; set; }


        [Required(ErrorMessage = "Password required!")]
        [MinLength(4, ErrorMessage = "Password must be at least 4 characters long!")]
        public string Password { get; set; }

        
    }
}
