using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleRegistrationReaderService.Models.ResponseClasses
{
    public class CombinedData
    {
        public DocumentDataResponse DocumentData { get; set; }
        public VehicleDataResponse VehicleData { get; set; }
        public PersonalDataResponse PersonalData { get; set; }
    }
}
