using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleRegistrationReaderService.Models.Exceptions
{
    public class VehicleRegistrationReaderException : Exception
    {
        public string Method { get; set; }

        public UInt32 Status { get; set; }

        public string StatusMessage { get; set; }

        public string DisplayMessage { get; set; }

        public VehicleRegistrationReaderException()
        {
        }

        public VehicleRegistrationReaderException(string message) : base(message)
        {
        }

        public VehicleRegistrationReaderException(string message, Exception inner) : base(message, inner)
        {
        }

    }
}
