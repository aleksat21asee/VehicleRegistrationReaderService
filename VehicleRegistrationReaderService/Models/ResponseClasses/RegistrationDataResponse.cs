using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleRegistrationReaderService.Models.ResponseClasses
{
    public class RegistrationDataResponse
    {
        public string RegistrationData { get; set; }

        public string SignatureData { get; set; }

        public string IssuingAuthority { get; set; }
    }
}
