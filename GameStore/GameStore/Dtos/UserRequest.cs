﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Dtos
{
    public class UserRequest:LightUserRequest
    {
        public int Id { get; set; }
        public string Password { get; set; }
    }
}
