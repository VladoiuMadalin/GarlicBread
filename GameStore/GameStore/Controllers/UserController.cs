using GameStore.Dtos;
using GameStore.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private GameStoreContext _dbContext;
        public UserController(GameStoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("GetUsers")]
        public IActionResult Get()
        {
            
            try
            {
                var users = _dbContext.Users.ToList(); 
                if(users.Count==0)
                {
                    return StatusCode(404, "No user found");
                }

                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error a aparut");
            }
        }

        [HttpPost("CreateUser")]
        public IActionResult Create([FromBody] UserRequest userRequest)
        {
            UserEntity user = new UserEntity()
            {
                Username = userRequest.Username,
                Email = userRequest.Email,
                PasswordHash = userRequest.Password
            };

            try
            {
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error a aparut");
            }

            var users = _dbContext.Users.ToList();
            return Ok(users);
        }

        [HttpPut("UpdateUser")]
        public IActionResult Update([FromBody] UserRequest userRequest)
        {
            try
            {
            var user = _dbContext.Users.FirstOrDefault(x => x.Id == userRequest.Id);
            if(user == null)
            {
                return StatusCode(404, "No user found");
            }
            user.Username = userRequest.Username;
            user.Email = userRequest.Email;
            user.PasswordHash = userRequest.Password;
            _dbContext.Entry(user).State =EntityState.Modified;
            _dbContext.SaveChanges();

            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error a aparut");
            }
            var users = _dbContext.Users.ToList();
            return Ok(users);
        }

        [HttpDelete("DeleteUser/{Id}")]
        public IActionResult Delete([FromRoute]int Id)
        {
            try
            {
                var user = _dbContext.Users.FirstOrDefault(x => x.Id == Id);
                if(user==null)
                {
                    return StatusCode(404, "No user found");
                }

                _dbContext.Entry(user).State = EntityState.Deleted;
                _dbContext.SaveChanges();

            }
            catch (Exception ex)
            {

                return StatusCode(500, "An error a aparut");
            }

            var users = _dbContext.Users.ToList();
            return Ok(users);
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
