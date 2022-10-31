using System;
using System.Runtime.InteropServices;

namespace VehicleRegistrationReaderService.MUP
{
    public class VehicleRegistrationAPI
    {
        public const string DLL_native = @"D:\VehicleRegistrationReaderService\eVehicleRegistrationAPI\eVehicleRegistrationCOM.dll";
        public const string DLL_com = @"D:\VehicleRegistrationReaderService\eVehicleRegistrationAPI\eVehicleRegistrationAPI.dll";

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

        public static string TestAttribute { get; set; }

        [DllImport(DLL_com)] public static extern UInt32 sdStartup(Int32 version);

        [DllImport(DLL_com)] public static extern UInt32 sdSleanup();

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
