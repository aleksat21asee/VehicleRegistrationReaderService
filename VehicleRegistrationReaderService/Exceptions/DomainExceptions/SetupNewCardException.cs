using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleRegistrationReaderService.Exceptions.DomainExceptions
{
    public class SetupNewCardException : BadRequestException
    {
        public SetupNewCardException() : 
            base ("Error while processing new card")
        {
        }
    }
}
