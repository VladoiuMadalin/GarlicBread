using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GameStore.Exceptions
{
    public class TitleExistsException : Exception
    {
        public TitleExistsException()
        {
        }

        public TitleExistsException(string message) : base(message)
        {
        }

        public TitleExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
