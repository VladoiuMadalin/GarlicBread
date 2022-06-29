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
    [Route("api/creditCard")]
    public class CreditCardController: WebApiController
    {
        private readonly IUnitOfWork _unitOfWork;
       

        public CreditCardController(IUnitOfWork unitOfWork )
        {
            _unitOfWork = unitOfWork;
            
        }


        [HttpPost]
        [Route("create")]
        //[Authorize(Roles = "User")]

        //public async Task<ActionResult<bool>> Register([FromBody] OrderRequest request)
        public async Task<ActionResult<bool>> CreateCredit([FromBody][Required] CreditCardRequest request)
        {
            if (request == null) return BadRequest("Empty Credit Card Data");

            var user = _unitOfWork.Users.GetById((Guid)GetUserId());
            if (user == null) return BadRequest();

            var newCreditCard = new CreditCardEntity()
            {
                User= user,
                CardNumber=request.CardNumber,
                NameOnCard=request.NameOnCard,
                ExpirationMonth=request.ExpirationMonth,
                ExpirationYear=request.ExpirationYear,
                Cvc=request.Cvc
            };

            try
            {
                _unitOfWork.CreditCards.Insert(newCreditCard);
                var saveResult = await _unitOfWork.SaveChangesAsync();
                return Ok(saveResult);
            }
            catch (CreditCardUserExistsException)
            {
                return BadRequest("Credit card for this user exists!");
            }
        }

        [HttpGet]
        [Route("all")]
        //[Authorize(Roles = "User")]
        public ActionResult<List<CreditCardDto>> GetAllCreditCards()
        {
            var user = _unitOfWork.Users.GetById((Guid)GetUserId());
            if (user == null) return BadRequest();

            var creditCards = _unitOfWork.CreditCards.GetAll(includeDeleted: false).Select(c => new CreditCardDto
            {
                CardNumber = c.CardNumber,
                NameOnCard = c.NameOnCard,
                Cvc = c.Cvc,
                ExpirationMonth = c.ExpirationMonth,
                ExpirationYear = c.ExpirationYear,
                User = user

            });
            return Ok(creditCards);
        }

        [HttpDelete]
        //[Authorize(Roles = "User")]
        [Route("deleteAll")]
        public async Task<ActionResult<List<CreditCardEntity>>> DeleteAllCreditCards()
        {

            var user = _unitOfWork.Users.GetById((Guid)GetUserId());
            if (user == null) return BadRequest();
            var creditCards = _unitOfWork.CreditCards.GetAll().Where(c => c.User == user).ToList(); //?????

            foreach (var credit in creditCards)
            {
                _unitOfWork.CreditCards.Delete(credit);
            }
            await _unitOfWork.SaveChangesAsync();
            return Ok(creditCards);
        }




    }
}
