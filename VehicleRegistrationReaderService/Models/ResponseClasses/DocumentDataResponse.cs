using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleRegistrationReaderService.Models.ResponseClasses
{
    public class DocumentDataResponse
    {
        public string StateIssuing { get; set; }

        public string CompetentAuthority { get; set; }

        public string AuthorityIssuing { get; set; }

        public string UnambiguousNumber { get; set; }

        public string IssuingDate { get; set; }

        public string ExpiryDate { get; set; }

        public string SerialNumber { get; set; }
    }
}
