using GameStore.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("GetUsers")]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpPost("CreateUser")]
        public IActionResult Create([FromBody] UserRequest userRequest)
        {
            return Ok();
        }

        [HttpPut("UpdateUser")]
        public IActionResult Update([FromBody] UserRequest userRequest)
        {
            return Ok();
        }

        [HttpDelete("DeleteUser/{Id}")]
        public IActionResult Delete(int Id)
        {
            return Ok();
        }

        private List<UserRequest> GetUsers()
        {
            return new List<UserRequest>()
            {
                new UserRequest {Id = 1,Username = "madalin" , Password="123"},
                new UserRequest {Id = 2,Username = "vladut" , Password="102"},
            };
        }
    }
}
