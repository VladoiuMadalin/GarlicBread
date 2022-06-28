using GameStore.DataLayer.Entities;
using GameStore.DataLayer.Repositories;
using GameStore.Dtos;
using GameStore.Exceptions;
using GameStore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : WebApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerAuthService _authorization;

        public UserController(IUnitOfWork unitOfWork, ICustomerAuthService authorization)
        {
            _unitOfWork = unitOfWork;
            _authorization = authorization;
        }



        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<bool>> Register([FromBody] RegisterRequest request)
        {
            if (request == null)
            {
                BadRequest(error: "Request must not be empty!");
            }

            var hashedPassword = _authorization.HashPassword(request.Password);

            var user = new UserEntity()
            {
                Username = request.Username,
                PasswordHash = hashedPassword,
                Email = request.Email,
            };

            try
            {
                _unitOfWork.Users.Insert(user);
                var saveResult = await _unitOfWork.SaveChangesAsync();
                return Ok(saveResult);
            }
            catch (UserExistsException)
            {
                return BadRequest("Username exists!");
            }
            catch (EmailExistsException)
            {
                return BadRequest("Email exists!");
            }

        }

        [HttpPost]
        [Route("login")]
        public ActionResult<ResponseLogin> Login([FromBody] LoginRequest request)
        {
            var user = _unitOfWork.Users.GetUserByUsername(request.Username);
            if (user == null) return BadRequest("User not found!");

            var samePassword = _authorization.VerifyHashedPassword(user.PasswordHash, request.Password);
            if (!samePassword) return BadRequest("Invalid password!");

            var user_jsonWebToken = _authorization.GetToken(user);

            return Ok(new ResponseLogin
            {
                Token = user_jsonWebToken
            });
        }

        [HttpGet]
        [Route("all")]
        //[Authorize]
        public ActionResult<List<LightUserRequest>> GetAll()
        {
            var users = _unitOfWork.Users.GetAll(includeDeleted: false).Select(u => new LightUserRequest
            {
                Username = u.Username,
                Email = u.Email
            }); ;
            return Ok(users);
        }

        [HttpGet]
        [Route("my-account")]
        // [Authorize(Roles = "User")]
        public ActionResult<bool> MyAccount()
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized();

            var user = _unitOfWork.Users.GetById((Guid)userId);


            return Ok(new UserRequest
            {
                Username = user.Username,
                Email = user.Email,

            });



        }
        [HttpDelete]
        //[Authorize(Roles = "Admin")]
        [Route("delete/all")]
        public async Task<ActionResult<List<UserEntity>>> DeleteAll()
        {
            var users = _unitOfWork.Users.GetAll().ToList();

            foreach (var user in users)
            {
                _unitOfWork.Users.Delete(user);
            }
            await _unitOfWork.SaveChangesAsync();
            return Ok(users);
        }






    }
}

//[Route("api/[controller]")]
//[ApiController]
//public class UserController : WebApiController
//{
//    private GameStoreContext _dbContext;
//    public UserController(GameStoreContext dbContext)
//    {
//        _dbContext = dbContext;
//        _dbContext.Database.EnsureDeleted();
//        _dbContext.Database.EnsureCreated();
//    }

//    [HttpGet("GetUsers")]
//    public IActionResult Get()
//    {

//        try
//        {
//            var users = _dbContext.Users.ToList();
//            if (users.Count == 0)
//            {
//                return StatusCode(404, "No user found");
//            }

//            return Ok(users);
//        }
//        catch (Exception ex)
//        {
//            return StatusCode(500, "An error a aparut");
//        }
//    }

//    [HttpPost("CreateUser")]
//    public IActionResult Create([FromBody] UserRequest userRequest)
//    {
//        UserEntity user = new UserEntity()
//        {
//            Username = userRequest.Username,
//            Email = userRequest.Email,
//            PasswordHash = userRequest.Password
//        };

//        try
//        {
//            _dbContext.Users.Add(user);
//            _dbContext.SaveChanges();
//        }
//        catch (Exception ex)
//        {
//            return StatusCode(500, "An error a aparut");
//        }

//        var users = _dbContext.Users.ToList();
//        return Ok(users);
//    }

//    [HttpPut("UpdateUser")]
//    public IActionResult Update([FromBody] UserRequest userRequest)
//    {
//        try
//        {
//            var user = _dbContext.Users.FirstOrDefault(x => x.Id == userRequest.Id);
//            if (user == null)
//            {
//                return StatusCode(404, "No user found");
//            }
//            user.Username = userRequest.Username;
//            user.Email = userRequest.Email;
//            user.PasswordHash = userRequest.Password;
//            _dbContext.Entry(user).State = EntityState.Modified;
//            _dbContext.SaveChanges();

//        }
//        catch (Exception ex)
//        {
//            return StatusCode(500, "An error a aparut");
//        }
//        var users = _dbContext.Users.ToList();
//        return Ok(users);
//    }

//    [HttpDelete("DeleteUser/{Id}")]
//    public IActionResult Delete([FromRoute] int Id)
//    {
//        try
//        {
//            var user = _dbContext.Users.FirstOrDefault(x => x.Id == Id);
//            if (user == null)
//            {
//                return StatusCode(404, "No user found");
//            }

//            _dbContext.Entry(user).State = EntityState.Deleted;
//            _dbContext.SaveChanges();

//        }
//        catch (Exception ex)
//        {

//            return StatusCode(500, "An error a aparut");
//        }

//        var users = _dbContext.Users.ToList();
//        return Ok(users);
//    }

//    private List<UserRequest> GetUsers()
//    {
//        return new List<UserRequest>()
//        {
//            new UserRequest {Id = 1,Username = "madalin" , Password="123"},
//            new UserRequest {Id = 2,Username = "vladut" , Password="102"},
//        };
//    }


