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
    [Route("api/shopping-cart")]
    public class ShoppingCartController : WebApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        

        public ShoppingCartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
   
        }


        [HttpPost]
        [Route("add")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult> AddOrder([FromBody][Required] LightShoppingCartDto shoppingCartDto)
        {
            if (shoppingCartDto == null) return BadRequest("Empty order");

            var user = _unitOfWork.Users.GetById((Guid)GetUserId());
            if (user == null) return BadRequest();

            var shoppingCart = new ShoppingCart() { User = user };
            var products = new List<Product>();

            decimal sumTotal = 0;
            foreach (var productDto in shoppingCartDto.Products)
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
            shoppingCart.Products = products;
            shoppingCart.TotalPrice = sumTotal;

            _unitOfWork.ShoppingCarts.Insert(shoppingCart);
            await _unitOfWork.SaveChangesAsync();

            return Ok();
        }


        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("delete-all")]
        public async Task<ActionResult<List<Order>>> DeleteAllShoppingCarts()
        {
            var user = _unitOfWork.Users.GetById((Guid)GetUserId());
            if (user == null) return BadRequest();

            var orders = _unitOfWork.ShoppingCarts.GetAll().Where(o => o.User == user)/*.ToList()*/; //?????

            foreach (var shoppingCart in orders)
            {
                _unitOfWork.ShoppingCarts.Delete(shoppingCart);
            }
            await _unitOfWork.SaveChangesAsync();
            return Ok(orders);
        }




    }
}
