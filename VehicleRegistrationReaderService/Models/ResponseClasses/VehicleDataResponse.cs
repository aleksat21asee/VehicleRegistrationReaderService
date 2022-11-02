using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleRegistrationReaderService.Models.ResponseClasses
{
    public class VehicleDataResponse
    {
        public string DateOfFirstRegistration { get; set; }

        public string YearOfProduction { get; set; }

        public string VehicleMake { get; set; }

        public string VehicleType { get; set; }

        public string CommercialDescription { get; set; }

        public string VehicleIDNumber { get; set; }

        public string RegistrationNumberOfVehicle { get; set; }

        public string MaximumNetPower { get; set; }

        public string EngineCapacity { get; set; }

        public string TypeOfFuel { get; set; }

        public string PowerWeightRatio { get; set; }

        public string VehicleMass { get; set; }

        public string MaximumPermissibleLadenMass { get; set; }

        public string TypeApprovalNumber { get; set; }

        public string NumberOfSeats { get; set; }

        public string NumberOfStandingPlaces { get; set; }

        public string EngineIDNumber { get; set; }

        public string NumberOfAxles { get; set; }

        public string VehicleCategory { get; set; }

        public string ColourOfVehicle { get; set; }

        public string RestrictionToChangeOwner { get; set; }

        public string VehicleLoad { get; set; }
    }
}
