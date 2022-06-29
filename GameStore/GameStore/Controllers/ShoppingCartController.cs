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
    public class ShoppingCartController : WebApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerAuthService _authorization;

        public ShoppingCartController(IUnitOfWork unitOfWork, ICustomerAuthService authorization)
        {
            _unitOfWork = unitOfWork;
            _authorization = authorization;
        }


        [HttpPost]
        [Route("createOrder")]
        //public async Task<ActionResult<bool>> Register([FromBody] OrderRequest request)
        public async Task<ActionResult<bool>> CreateShoppingCart(UserEntity user)
        {


            var shoppingCart = new ShoppingCartEntity()
            {
                User = user,
                TotalPrice = 0

            };

            try
            {
                _unitOfWork.ShoppingCarts.Insert(shoppingCart);
                var saveResult = await _unitOfWork.SaveChangesAsync();
                return Ok(saveResult);
            }
            catch (ShoppingCartForUserExistsException)
            {
                return BadRequest("Shopping Cart for this user exists!");
            }
        }

        [HttpGet]
        [Route("allShoppingCarts")]
        //[Authorize]
        public ActionResult<List<OrderRequest>> GetAllShoppingCarts(UserEntity user)
        {
            var orders = _unitOfWork.Orders.GetAll(includeDeleted: false).Select(o => new OrderRequest  //basically la fel
            {
                Products = o.Products,
                TotalPrice = o.TotalPrice,
                User=o.User
            }).Where(o => o.User == user); //???? 
            return Ok(orders);
        }

        [HttpDelete]
        //[Authorize(Roles = "Admin")]
        [Route("deleteAllShoppingCarts")]
        public async Task<ActionResult<List<ShoppingCartEntity>>> DeleteAllShoppingCarts(UserEntity user)
        {
            var shoppingCarts = _unitOfWork.ShoppingCarts.GetAll().Where(s => s.User == user).ToList(); //?????

            foreach (var shoppingCart in shoppingCarts)
            {
                _unitOfWork.ShoppingCarts.Delete(shoppingCart);
            }
            await _unitOfWork.SaveChangesAsync();
            return Ok(shoppingCarts);
        }




    }
}
