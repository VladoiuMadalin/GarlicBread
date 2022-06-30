using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Dtos
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Email required!")]
        [MaxLength(100, ErrorMessage = "Email too long!")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Username required!")]
        [MaxLength(30, ErrorMessage = "Username too long!")]
        public string Username { get; set; }


        [Required(ErrorMessage = "Password required!")]
        [MinLength(4, ErrorMessage = "Password must be at least 4 characters long!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Role required!")]
        [MaxLength(10, ErrorMessage = "Role too long")]
        public string Role { get; set; }
    }
}
