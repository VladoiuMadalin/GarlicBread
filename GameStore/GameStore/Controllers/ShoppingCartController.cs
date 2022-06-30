using GameStore.DataLayer.Entities;
using GameStore.DataLayer.Repositories;
using GameStore.Dtos;
using GameStore.Exceptions;
using GameStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace GameStore.Controllers
{
    [ApiController]
    [Route("api/shoppingCart")]
    public class ShoppingCartController : WebApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        

        public ShoppingCartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
   
        }


        [HttpPost]
        //[Authorize(Roles = "User")]
        [Route("create")]
        //public async Task<ActionResult<bool>> Register([FromBody] OrderRequest request)
        public async Task<ActionResult<bool>> CreateShoppingCart([FromBody][Required] ShoppingCartRequest request)
        {
            if (request == null) return BadRequest("Empty Shopping Cart");

            var user = _unitOfWork.Users.GetById((Guid)GetUserId());
            if (user == null) return BadRequest();

            
            var products = new List<ProductEntity>();

            decimal sumTotal = 0;
            foreach (var productDto in request.Products)
            {
                try
                {
                    products.Add(_unitOfWork.Products.GetProductByTitle(productDto.Title));
                    sumTotal += _unitOfWork.Products.GetProductByTitle(productDto.Title).Price;

                }
                catch (InvalidOperationException)
                {
                    return BadRequest("some products don't exist");
                }
            }


            var shoppingCart = new ShoppingCartEntity()
            {
                User = user,
                TotalPrice = sumTotal,
                Products=products
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
        [Route("all")]
        //[Authorize(Roles = "User")]
        public ActionResult<List<ShoppingCartDto>> GetAllShoppingCarts()
        {
            var user = _unitOfWork.Users.GetById((Guid)GetUserId());
            if (user == null) return BadRequest();

            var shoppingCart = _unitOfWork.Orders.GetAll(includeDeleted: false).Select(s => new ShoppingCartDto  
            {
                Products = s.Products,
                TotalPrice = s.TotalPrice,
                User = s.User
            });//???? 
            return Ok(shoppingCart);
        }

        [HttpDelete]
        //[Authorize(Roles = "User")]
        [Route("delete")]
        public async Task<ActionResult<List<ShoppingCartEntity>>> DeleteAllShoppingCarts()
        {
            var user = _unitOfWork.Users.GetById((Guid)GetUserId());
            if (user == null) return BadRequest();

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
