using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleRegistrationReaderService.Exceptions.DomainExceptions
{
    public class CleanupException : BadRequestException
    {
        public CleanupException(string message)
            : base(message)
        {
        }
    }
}
