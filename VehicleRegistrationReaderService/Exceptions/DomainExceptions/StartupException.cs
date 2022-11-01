using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleRegistrationReaderService.Exceptions.DomainExceptions
{
    public class StartupException : BadRequestException
    {
        public StartupException(string message)
            : base(message)
        {
        }
    }
}
