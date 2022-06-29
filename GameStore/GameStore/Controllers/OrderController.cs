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
        [Route("createOrder")]
        //public async Task<ActionResult<bool>> Register([FromBody] OrderRequest request)
        public async Task<ActionResult<bool>> CreateOrder(UserEntity user)
        {
           

            var order = new OrderEntity()
            {
                User = user,
                TotalPrice =0
                
            };

            try
            {
                _unitOfWork.Orders.Insert(order);
                var saveResult = await _unitOfWork.SaveChangesAsync();
                return Ok(saveResult);
            }
            catch (OrderForUserExistsException)
            {
                return BadRequest("Order for this user exists!");
            }
        }

        [HttpGet]
        [Route("allOrders")]
        //[Authorize]
        public ActionResult<List<OrderRequest>> GetAllOrders(UserEntity user)
        {
            var orders= _unitOfWork.Orders.GetAll(includeDeleted: false).Select(o => new OrderRequest
            {
                Products= o.Products,
                TotalPrice = o.TotalPrice,
                User = o.User

            }).Where(o=>o.User==user); //???? 
            return Ok(orders);
        }

        [HttpDelete]
        //[Authorize(Roles = "Admin")]
        [Route("deleteAllOrders")]
        public async Task<ActionResult<List<OrderEntity>>> DeleteAllOrders(UserEntity user)
        {
            var orders = _unitOfWork.Orders.GetAll().Where(o=>o.User==user).ToList(); //?????

            foreach (var order in orders)
            {
                _unitOfWork.Orders.Delete(order);
            }
            await _unitOfWork.SaveChangesAsync();
            return Ok(orders);
        }




    }
}
