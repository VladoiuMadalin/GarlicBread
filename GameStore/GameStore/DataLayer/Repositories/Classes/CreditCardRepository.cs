using GameStore.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameStore.Exceptions;


namespace GameStore.DataLayer.Repositories
{
    public class CreditCardRepository : BaseRepository<CreditCard>,ICreditCardRepository
    {
        public CreditCardRepository(GameStoreContext context) : base(context)
        {
        }

        public CreditCard GetCreditCardbyUser(User user)
        {
            CreditCard result = GetRecords().FirstOrDefault(creditCard => creditCard.User == user);
            return result;
        }

        public override void Insert(CreditCard record)
        {
            var CreditCardUserExists = _dbSet.Any(x => x.User == record.User);

            if (CreditCardUserExists)
            {
                base.Update(record);
            }

            if (_db.Entry(record).State == EntityState.Detached)
            {
                _db.Attach(record);
                _db.Entry(record).State = EntityState.Added;
            }


        }


    }
}
