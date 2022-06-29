using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Exceptions
{
    public class CreditCardUserExistsException : Exception
    {
        public CreditCardUserExistsException()
        {
        }

        public CreditCardUserExistsException(string message) : base(message)
        {
        }

        public CreditCardUserExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
