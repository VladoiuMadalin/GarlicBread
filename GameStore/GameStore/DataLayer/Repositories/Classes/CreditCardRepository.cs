using GameStore.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.Exceptions;
using GameStore.DataLayer.Repositories.Interfaces;

namespace GameStore.DataLayer.Repositories.Classes
{
    public class CreditCardRepository : RepositoryBase<CreditCardEntity>,ICreditCardRepository
    {
        public CreditCardRepository(GameStoreContext context) : base(context)
        {
        }

        public CreditCardEntity GetCreditCardbyUser(UserEntity user)
        {
            CreditCardEntity result = GetRecords().FirstOrDefault(creditCard => creditCard.User == user);
            return result;
        }

        public override void Insert(CreditCardEntity record)
        {
            var CreditCardUserExists = _dbSet.Any(x => x.User == record.User);

            if (CreditCardUserExists)
            {
                throw new CreditCardUserExistsException();
            }

            if (_db.Entry(record).State == EntityState.Detached)
            {
                _db.Attach(record);
                _db.Entry(record).State = EntityState.Added;
            }


        }


    }
}
