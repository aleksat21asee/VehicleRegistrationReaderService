using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleRegistrationReaderService.Exceptions.DomainExceptions
{
    public class ReaderNotFoundException : NotFoundException
    {
        public ReaderNotFoundException(string readerName) 
            : base($"Reader with name [{readerName}] can't be accessed")
        {
        }
    }
}
