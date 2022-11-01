using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleRegistrationReaderService.Exceptions
{
    public class WrapperException : Exception
    {
        public WrapperException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
