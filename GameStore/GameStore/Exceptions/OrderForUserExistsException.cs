using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GameStore.Exceptions
{
    public class OrderForUserExistsException : Exception
    {
        public OrderForUserExistsException()
        {
        }

        public OrderForUserExistsException(string message) : base(message)
        {
        }

        public OrderForUserExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
