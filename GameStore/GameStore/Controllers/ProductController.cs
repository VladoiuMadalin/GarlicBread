
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
using System.Linq;
using System.Threading.Tasks;


namespace GameStore.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : WebApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        // private readonly ICustomerAuthService _authorization;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //_authorization = authorization;
        }


        [HttpPost]
        [Route("add")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> AddProduct([FromBody] ProductDto request)
        {
            if (request == null)
            {
                BadRequest(error: "Request must not be empty!");
            }

            var product = new Product()
            {
                Title = request.Title,
                Price = request.Price,
                Picture = request.Picture
            };

            try
            {
                _unitOfWork.Products.Insert(product);
                var saveResult = await _unitOfWork.SaveChangesAsync();
                return Ok(saveResult);
            }
            catch (TitleExistsException)
            {
                return BadRequest("Title exists!");
            }



        }

        [HttpGet]
        [Route("all")]
        [Authorize]
        public ActionResult<List<ProductDto>> GetAllProducts()
        {
            var products = _unitOfWork.Products.GetAll(includeDeleted: false).Select(p => new ProductDto
            {
                Title = p.Title,
                Price = p.Price,
                Picture = p.Picture,
                Id = p.Id
            });
            return Ok(products);
        }


        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("delete-all")]
        public async Task<ActionResult<List<Product>>> DeleteAllProducts()
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
        [Authorize(Roles = "Admin")]
        [Route("delete")]
        public async Task<ActionResult<List<Product>>> DeleteAProduct([FromBody] LightProductDto request)
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

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("deleteById")]
        public async Task<ActionResult<List<Product>>> DeleteById([FromBody] DeleteRequest request)
        {
            try
            {
                _unitOfWork.Products.DeleteById(request.Id);
                await _unitOfWork.SaveChangesAsync();
                return Ok();
            }
            catch (InvalidOperationException)
            {
                return BadRequest("Id doesn't exist!");
            }
        }
    }
}