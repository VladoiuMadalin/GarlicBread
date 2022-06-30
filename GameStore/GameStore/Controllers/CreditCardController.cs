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
    public class CreditCardController : WebApiController
    {
        private readonly IUnitOfWork _unitOfWork;


        public CreditCardController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }


        [HttpPut]
        [Route("create")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<bool>> CreateCredit([FromBody][Required] LightCreditCardDto request)
        {
            if (request == null) return BadRequest("Empty Credit Card Data");

            var user = _unitOfWork.Users.GetById((Guid)GetUserId());
            if (user == null) return BadRequest();

            var creditCard = _unitOfWork.CreditCards.GetCreditCardbyUser(user);
            if (creditCard == null)
            {
                creditCard = new CreditCard() { User = user };
            }

            creditCard.CardNumber = request.CardNumber;
            creditCard.NameOnCard = request.NameOnCard;
            creditCard.ExpirationMonth = request.ExpirationMonth;
            creditCard.ExpirationYear = request.ExpirationYear;
            creditCard.Cvc = request.Cvc;


            try
            {
                _unitOfWork.CreditCards.Insert(creditCard);

            }
            catch (CreditCardUserExistsException)
            {
                _unitOfWork.CreditCards.Update(creditCard);
            }
            var saveResult = await _unitOfWork.SaveChangesAsync();
            return Ok(saveResult);

        }

        [HttpGet]
        [Route("all")]
        [Authorize(Roles = "User")]
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
                User = new LightUserDto { Email = user.Email, Username = user.Username }

            });
            return Ok(creditCards);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("deleteAll")]
        public async Task<ActionResult<List<CreditCard>>> DeleteAllCreditCards()
        {

            var user = _unitOfWork.Users.GetById((Guid)GetUserId());
            if (user == null) return BadRequest();
            var creditCards = _unitOfWork.CreditCards.GetAll().Where(c => c.User == user);

            foreach (var credit in creditCards)
            {
                _unitOfWork.CreditCards.Delete(credit);
            }
            await _unitOfWork.SaveChangesAsync();
            return Ok(creditCards);
        }




    }
}
