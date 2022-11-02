using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using VehicleRegistrationReaderService.MUP;

namespace VehicleRegistrationReaderService.Models.MarshallStructs
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct VehicleDataMUP
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = VehicleRegistrationAPI.MaxDateOfFirstRegistrationSize)]
        public string DateOfFirstRegistration;
        public Int32 DateOfFirstRegistrationSize;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = VehicleRegistrationAPI.MaxYearOfProductionSize)]
        public string YearOfProduction;
        public Int32 YearOfProductionSize;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = VehicleRegistrationAPI.MaxVehicleMakeSize)]
        public string VehicleMake;
        public Int32 VehicleMakeSize;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = VehicleRegistrationAPI.MaxVehicleTypeSize)]
        public string VehicleType;
        public Int32 VehicleTypeSize;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = VehicleRegistrationAPI.MaxVehicleTypeSize)]
        public string CommercialDescription;
        public Int32 CommercialDescriptionSize;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = VehicleRegistrationAPI.MaxVehicleIDNumberSize)]
        public string VehicleIDNumber;
        public Int32 VehicleIDNumberSize;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = VehicleRegistrationAPI.MaxRegistrationNumberOfVehicleSize)]
        public string RegistrationNumberOfVehicle;
        public Int32 RegistrationNumberOfVehicleSize;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = VehicleRegistrationAPI.MaxMaximumNetPowerSize)]
        public string MaximumNetPower;
        public Int32 MaximumNetPowerSize;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = VehicleRegistrationAPI.MaxEngineCapacitySize)]
        public string EngineCapacity;
        public Int32 EngineCapacitySize;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = VehicleRegistrationAPI.MaxTypeOfFuelSize)]
        public string TypeOfFuel;
        public Int32 TypeOfFuelSize;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = VehicleRegistrationAPI.MaxPowerWeightRatioSize)]
        public string PowerWeightRatio;
        public Int32 PowerWeightRatioSize;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = VehicleRegistrationAPI.MaxVehicleMassSize)]
        public string VehicleMass;
        public Int32 VehicleMassSize;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = VehicleRegistrationAPI.MaxMaximumPermissibleLadenMassSize)]
        public string MaximumPermissibleLadenMass;
        public Int32 MaximumPermissibleLadenMassSize;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = VehicleRegistrationAPI.MaxTypeApprovalNumberSize)]
        public string TypeApprovalNumber;
        public Int32 TypeApprovalNumberSize;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = VehicleRegistrationAPI.MaxNumberOfSeatsSize)]
        public string NumberOfSeats;
        public Int32 NumberOfSeatsSize;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = VehicleRegistrationAPI.MaxNumberOfStandingPlacesSize)]
        public string NumberOfStandingPlaces;
        public Int32 NumberOfStandingPlacesSize;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = VehicleRegistrationAPI.MaxEngineIDNumberSize)]
        public string EngineIDNumber;
        public Int32 EngineIDNumberSize;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = VehicleRegistrationAPI.MaxNumberOfAxlesSize)]
        public string NumberOfAxles;
        public Int32 NumberOfAxlesSize;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = VehicleRegistrationAPI.MaxVehicleCategorySize)]
        public string VehicleCategory;
        public Int32 VehicleCategorySize;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = VehicleRegistrationAPI.MaxColourOfVehicleSize)]
        public string ColourOfVehicle;
        public Int32 ColourOfVehicleSize;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = VehicleRegistrationAPI.MaxRestrictionToChangeOwnerSize)]
        public string RestrictionToChangeOwner;
        public Int32 RestrictionToChangeOwnerSize;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = VehicleRegistrationAPI.MaxVehicleLoadSize)]
        public string VehicleLoad;
        public Int32 VehicleLoadSize;
    }
}
