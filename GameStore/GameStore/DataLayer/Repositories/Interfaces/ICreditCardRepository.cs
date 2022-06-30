using GameStore.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.DataLayer.Repositories
{
    public interface ICreditCardRepository : IBaseRepository<CreditCard>
    {
        CreditCard GetCreditCardbyUser(User user);
    }
}
