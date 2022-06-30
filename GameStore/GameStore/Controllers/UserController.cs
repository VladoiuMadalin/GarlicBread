using GameStore.DataLayer.Entities;
using GameStore.DataLayer.Repositories;
using GameStore.Dtos;
using GameStore.Enums;
using GameStore.Exceptions;
using GameStore.Services;
using Microsoft.AspNetCore.Authorization;
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

            Role role;
            try
            {
                role = Enum.Parse<Role>(request.Role);
            }
            catch
            {
                return BadRequest("Invalid role!");
            }


            var user = new User()
            {
                Username = request.Username,
                PasswordHash = hashedPassword,
                Email = request.Email,
                Role = role,
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

        [HttpPut]
        [Route("change-password")]
        [Authorize]
        public async Task<ActionResult<bool>> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var user = _unitOfWork.Users.GetById((Guid)GetUserId());

            if (user == null) return BadRequest("User not found!");

            //var oldPasswordHashed = _authorization.HashPassword(request.OldPassword);
            var samePassword = _authorization.VerifyHashedPassword(user.PasswordHash, request.OldPassword);

            if (!samePassword) return BadRequest("Invalid password!");

            var newPasswordHashed = _authorization.HashPassword(request.NewPassword);
            user.PasswordHash = newPasswordHashed;

            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        [Route("all")]
        //[Authorize(Roles = "User")]
        public ActionResult<List<LightUserDto>> GetAll()
        {
            var users = _unitOfWork.Users.GetAll(includeDeleted: false).Select(u => new LightUserDto
            {
                Username = u.Username,
                Email = u.Email
            }); ;
            return Ok(users);
        }

        [HttpGet]
        [Route("my-account")]
        [Authorize]
        public ActionResult<bool> MyAccount()
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized();

            var user = _unitOfWork.Users.GetAccount((Guid)userId);


            return Ok(new UserRequest
            {
                Username = user.Username,
                Email = user.Email,
                Orders = user.Orders.Select(o => new OrderDto
                {
                    TotalPrice = o.TotalPrice,
                    Products = o.Products.Select(p => new ProductDto { Title = p.Title, Price = p.Price, Picture = p.Picture, Id = p.Id }).ToList(),
                    Id = o.Id
                }).ToList()
            });



        }
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("delete/all")]
        public async Task<ActionResult<List<User>>> DeleteAll()
        {
            var users = _unitOfWork.Users.GetAll().ToList();

            foreach (var user in users)
            {
                _unitOfWork.Users.Delete(user);

                foreach (var order in user.Orders)
                {
                    _unitOfWork.Orders.Delete(order);
                }
                foreach (var shoppingCart in user.ShoppingCarts)
                {
                    _unitOfWork.ShoppingCarts.Delete(shoppingCart);
                }
            }
            await _unitOfWork.SaveChangesAsync();
            return Ok(users);
        }


        [HttpDelete]
        [Authorize]
        [Route("delete/this")]
        public async Task<ActionResult<User>> DeleteUser()
        {
            _unitOfWork.Users.DeleteById((Guid)GetUserId());
            await _unitOfWork.SaveChangesAsync();
            return Ok();
        }









    }
}

