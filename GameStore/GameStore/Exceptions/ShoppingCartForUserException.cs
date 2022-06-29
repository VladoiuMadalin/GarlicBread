using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Exceptions
{
    public class ShoppingCartForUserExistsException : Exception
    {
        public ShoppingCartForUserExistsException()
        {
        }

        public ShoppingCartForUserExistsException(string message) : base(message)
        {
        }

        public ShoppingCartForUserExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
