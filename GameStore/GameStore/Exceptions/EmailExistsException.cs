using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Exceptions
{
    public class EmailExistsException : Exception
    {
        public EmailExistsException()
        {
        }

        public EmailExistsException(string message) : base(message)
        {
        }

        public EmailExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
