using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using VehicleRegistrationReaderService.MUP;

namespace VehicleRegistrationReaderService.Models.MarshallStructs
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct RegistrationDataMUP
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = VehicleRegistrationAPI.MaxRegistrationDataSize)]
        public string RegistrationData;
        public Int32 RegistrationDataSize;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = VehicleRegistrationAPI.MaxSignatureDataSize)]
        public string SignatureData;
        public Int32 SignatureDataSize;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = VehicleRegistrationAPI.MaxIssuingAuthoritySize)]
        public string IssuingAuthority;
        public Int32 IssuingAuthoritySize;
    }
}
