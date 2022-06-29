
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
    public class ProductController : WebApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerAuthService _authorization;

        public ProductController(IUnitOfWork unitOfWork, ICustomerAuthService authorization)
        {
            _unitOfWork = unitOfWork;
            _authorization = authorization;
        }


        [HttpPost]
        [Route("insertProduct")]
        public async Task<ActionResult<bool>> InsertProduct([FromBody] ProductRequest request)
        {
            if (request == null)
            {
                BadRequest(error: "Request must not be empty!");
            }

            var product = new ProductEntity()
            {
                Title = request.Title,
                Price = request.Price,
                //Orders=new ICollection<OrderEntity>
                //ShoppingCarts= new ICollection<ShoppingCartEntity>
            };

            try
            {
                _unitOfWork.Products.Insert(product);
                var saveResult = await _unitOfWork.SaveChangesAsync();
                return Ok(saveResult);
            }
            catch(TitleExistsException)
            {
                return BadRequest("Title exists!");
            }



        }

        [HttpGet]
        [Route("allProducts")]
        //[Authorize]
        public ActionResult<List<ProductRequest>> GetAllProducts()
        {
            var products = _unitOfWork.Products.GetAll(includeDeleted: false).Select(p => new ProductRequest
            {
                Title = p.Title,
                Price = p.Price
            }); ;
            return Ok(products);
        }


        [HttpDelete]
        //[Authorize(Roles = "Admin")]
        [Route("deleteAllProducts")]
        public async Task<ActionResult<List<ProductEntity>>> DeleteAllProducts()
        {
            var products = _unitOfWork.Products.GetAll().ToList();

            foreach (var product in products)
            {
                _unitOfWork.Products.Delete(product);
            }
            await _unitOfWork.SaveChangesAsync();
            return Ok(products);
        }

        [HttpDelete]
        //[Authorize(Roles = "Admin")]
        [Route("deleteOneProduct")]
        public async Task<ActionResult<List<ProductEntity>>> DeleteAProduct([FromBody] LightProductRequest request)
        {
            var products = _unitOfWork.Products.GetAll().ToList();
            string title = request.Title;
            foreach (var product in products)
            {
                if (product.Title == title)
                {   
                    _unitOfWork.Products.Delete(product);
                    break;
                }
            }
            await _unitOfWork.SaveChangesAsync();
            return Ok(products);
        }
    }
}