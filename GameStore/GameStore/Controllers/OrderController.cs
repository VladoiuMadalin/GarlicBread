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

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
           
        }


        [HttpPost]
        [Route("add")]
        //[Authorize(Roles = "User")]
        public async Task<ActionResult> AddOrder([FromBody][Required] OrderDto orderDto)
        {
            if (orderDto == null) return BadRequest("Empty order");

            var user = _unitOfWork.Users.GetById((Guid)GetUserId());
            if (user == null) return BadRequest();

            var order = new OrderEntity() { User = user };
            var products = new List<ProductEntity>();

            decimal sumTotal = 0;
            foreach (var productDto in orderDto.Products)
            {
                try
                {
                    products.Add(_unitOfWork.Products.GetProductByTitle(productDto.Title));
                    sumTotal += _unitOfWork.Products.GetProductByTitle(productDto.Title).Price;
                }
                catch(InvalidOperationException)
                {
                    return BadRequest("some products don't exist");
                }
            }
            order.Products = products;
            order.TotalPrice = sumTotal;

            _unitOfWork.Orders.Insert(order);
            await _unitOfWork.SaveChangesAsync();

            return Ok();
        }


        

        [HttpGet]
        [Route("all")]
        [Authorize]
        //[Authorize(Roles = "User")]
        public ActionResult<List<OrderRequest>> GetAllOrders()
        {
            var user = _unitOfWork.Users.GetById((Guid)GetUserId());
            if (user == null) return BadRequest();

            var orders = _unitOfWork.Orders.GetAll(includeDeleted: false).Select(o => new OrderRequest
            {
                Products = o.Products,
                TotalPrice = o.TotalPrice,
                User = o.User

            });
            return Ok(orders);
        }


        [HttpGet]
        [Route("allAdmin")]
        [Authorize(Roles = "Admin")]
        public ActionResult<List<OrderRequest>> GetAllOrdersAdmin()
        {

            var orders = _unitOfWork.Orders.GetAll(includeDeleted: false).Select(o => new OrderRequest
            {
                Products = o.Products,
                TotalPrice = o.TotalPrice,
                User = o.User

            });
            return Ok(orders);
        }



        [HttpDelete]
        //[Authorize(Roles = "User")]
        [Route("delete")]
        public async Task<ActionResult<List<OrderEntity>>> DeleteAllOrders()
        {
            var user = _unitOfWork.Users.GetById((Guid)GetUserId());
            if (user == null) return BadRequest();

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
