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
    [Route("api/orders")]
    public class OrderController : WebApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerAuthService _authorization;

        public OrderController(IUnitOfWork unitOfWork, ICustomerAuthService authorization)
        {
            _unitOfWork = unitOfWork;
            _authorization = authorization;
        }


        [HttpPost]
        [Route("add")]
        //[Authorize("User")]
        public async Task<ActionResult> AddOrder([FromBody][Required] OrderDto orderDto)
        {
            if (orderDto == null) return BadRequest("Empty notification");

            var user = _unitOfWork.Users.GetById((Guid)GetUserId());
            if (user == null) return BadRequest();

            var order = new OrderEntity() { User = user };
            var products = new List<ProductEntity>();

            foreach (var productDto in orderDto.Products)
            {
                try
                {
                    products.Add(_unitOfWork.Products.GetProductByTitle(productDto.Title));
                }
                catch(InvalidOperationException)
                {
                    return BadRequest("some products don't exist");
                }
            }
            order.Products = products;

            _unitOfWork.Orders.Insert(order);
            await _unitOfWork.SaveChangesAsync();

            return Ok();
        }


        

        [HttpGet]
        [Route("all")]
        [Authorize]
        public ActionResult<List<OrderRequest>> GetAllOrders(UserEntity user)
        {
            var orders = _unitOfWork.Orders.GetAll(includeDeleted: false).Select(o => new OrderRequest
            {
                Products = o.Products,
                TotalPrice = o.TotalPrice,
                User = o.User

            }).Where(o => o.User == user); //???? 
            return Ok(orders);
        }

        [HttpDelete]
        //[Authorize(Roles = "Admin")]
        [Route("deleteAllOrders")]
        public async Task<ActionResult<List<OrderEntity>>> DeleteAllOrders(UserEntity user)
        {
            var orders = _unitOfWork.Orders.GetAll().Where(o => o.User == user).ToList(); //?????

            foreach (var order in orders)
            {
                _unitOfWork.Orders.Delete(order);
            }
            await _unitOfWork.SaveChangesAsync();
            return Ok(orders);
        }




    }
}
