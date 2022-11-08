using System;
using System.Runtime.InteropServices;
using System.Text;
using VehicleRegistrationReaderService.Models;
using VehicleRegistrationReaderService.Models.MarshallStructs;

namespace VehicleRegistrationReaderService.MUP
{
    public class VehicleRegistrationAPI
    {
        //public const string DLL = @"D:\VehicleRegistrationReaderService\eVehicleRegistrationAPI\eVehicleRegistrationAPI.dll";
        public const string DLL = "eVehicleRegistrationAPI.dll";

        // max length values


        // personal data
        public const Int32 MaxOwnersPersonalNoSize = 20;
        public const Int32 MaxOwnersSurnameOrBusinessNameSize = 100;
        public const Int32 MaxOwnerNameSize = 100;
        public const Int32 MaxOwnerAddressSize = 200;
        public const Int32 MaxUsersPersonalNoSize = 20;
        public const Int32 MaxusersSurnameOrBusinessNameSize = 100;
        public const Int32 MaxUsersNameSize = 100;
        public const Int32 MaxUsersAddressSize = 200;

        // registration data
        public const Int32 MaxRegistrationDataSize = 4096;
        public const Int32 MaxSignatureDataSize = 1024;
        public const Int32 MaxIssuingAuthoritySize = 4096;

        // document data
        public const Int32 MaxStateIssuingSize = 50;
        public const Int32 MaxCompetentAuthoritySize = 50;
        public const Int32 MaxAuthorityIssuingSize = 50;
        public const Int32 MaxUnambiguousNumberSize = 30;
        public const Int32 MaxIssuingDateSize = 16;
        public const Int32 MaxExpiryDateSize = 16;
        public const Int32 MaxSerialNumberSize = 20;

        // vehicle data
        public const Int32 MaxDateOfFirstRegistrationSize = 16;
        public const Int32 MaxYearOfProductionSize = 5;
        public const Int32 MaxVehicleMakeSize = 100;
        public const Int32 MaxVehicleTypeSize = 100;
        public const Int32 MaxCommercialDescriptionSize = 100;
        public const Int32 MaxVehicleIDNumberSize = 100;
        public const Int32 MaxRegistrationNumberOfVehicleSize = 20;
        public const Int32 MaxMaximumNetPowerSize = 20;
        public const Int32 MaxEngineCapacitySize = 20;
        public const Int32 MaxTypeOfFuelSize = 100;
        public const Int32 MaxPowerWeightRatioSize = 20;
        public const Int32 MaxVehicleMassSize = 20;
        public const Int32 MaxMaximumPermissibleLadenMassSize = 20;
        public const Int32 MaxTypeApprovalNumberSize = 50;
        public const Int32 MaxNumberOfSeatsSize = 20;
        public const Int32 MaxNumberOfStandingPlacesSize = 20;
        public const Int32 MaxEngineIDNumberSize = 100;
        public const Int32 MaxNumberOfAxlesSize = 20;
        public const Int32 MaxVehicleCategorySize = 50;
        public const Int32 MaxColourOfVehicleSize = 50;
        public const Int32 MaxRestrictionToChangeOwnerSize = 200;
        public const Int32 MaxVehicleLoadSize = 20;

        // response codes
        public const UInt32 S_OK = 0;

        public const UInt32 ERROR_BAD_FORMAT = 11;
        public const UInt32 ERROR_INVALID_ACCESS = 12;
        public const UInt32 ERROR_INVALID_DATA = 13;
        public const UInt32 ERROR_INVALID_PARAMETER = 87;
        public const UInt32 ERROR_SERVICE_ALREADY_RUNNING = 1056;
        public const UInt32 ERROR_SERVICE_NOT_ACTIVE = 1062;
        public const UInt32 E_POINTER = 0x80004003;
        public const UInt32 SCARD_E_INSUFFICIENT_BUFFER = 0x80100008;
        public const UInt32 SCARD_E_UNKNOWN_READER = 0x80100009;
        public const UInt32 SCARD_E_NO_SMARTCARD = 0x8010000C;
        public const UInt32 SCARD_E_INVALID_VALUE = 0x80100011;
        public const UInt32 SCARD_E_READER_UNAVAILABLE = 0x80100017;
        public const UInt32 SCARD_E_CARD_UNSUPPORTED = 0x8010001C;
        public const UInt32 SCARD_E_NO_READERS_AVAILABLE = 0x8010002E;

        public const Int32 MUP_READER_LENGTH_SIZE = 250;

        [DllImport(DLL)]
        public static extern UInt32 sdStartup(Int32 version);

        [DllImport(DLL)] 
        public static extern UInt32 sdCleanup();

        [DllImport(DLL, CharSet = CharSet.Ansi)]
        public static extern UInt32 GetReaderName(UInt32 index, StringBuilder shortName, ref Int32 nameSize);

        [DllImport(DLL, CharSet = CharSet.Ansi)]
        public static extern UInt32 SelectReader(StringBuilder reader);

        [DllImport(DLL, CharSet = CharSet.Ansi)]
        public static extern UInt32 sdProcessNewCard();

        [DllImport(DLL)]
        public static extern UInt32 sdReadPersonalData(ref PersonalDataMUP personalDataMUP);

        [DllImport(DLL)]
        public static extern UInt32 sdReadRegistration(ref RegistrationDataMUP registrationDataMUP, UInt32 index);

        [DllImport(DLL)]
        public static extern UInt32 sdReadDocumentData(ref DocumentDataMUP documentDataMUP);

        [DllImport(DLL)]
        public static extern UInt32 sdReadVehicleData(ref VehicleDataMUP vehicleDataMUP);

        public static string ResponseMessage(UInt32 status)
        {
            return status switch
            {
                S_OK => "OK",
                ERROR_BAD_FORMAT => "Bad format",
                ERROR_INVALID_ACCESS => "Invalid access",
                ERROR_INVALID_DATA => "Invalid data",
                ERROR_INVALID_PARAMETER => "Invalid parameter",
                ERROR_SERVICE_ALREADY_RUNNING => "Service already running",
                ERROR_SERVICE_NOT_ACTIVE => "Service not active",
                E_POINTER => "Pointer",
                SCARD_E_INSUFFICIENT_BUFFER => "Insufficient buffer",
                SCARD_E_UNKNOWN_READER => "Unknown reader",
                SCARD_E_NO_SMARTCARD => "No smartcard",
                SCARD_E_INVALID_VALUE => "Invalid value",
                SCARD_E_READER_UNAVAILABLE => "Reader unavailable",
                SCARD_E_CARD_UNSUPPORTED => "Card unsupported",
                SCARD_E_NO_READERS_AVAILABLE => "No readers available",
                _ => "Unknown error"
            };
        }
    }
}
