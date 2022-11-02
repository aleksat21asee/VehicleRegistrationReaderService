using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using VehicleRegistrationReaderService.MUP;

namespace VehicleRegistrationReaderService.Models.MarshallStructs
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct DocumentDataMUP
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = VehicleRegistrationAPI.MaxStateIssuingSize)]
        public string StateIssuing;
        public Int32 StateIssuingSize;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = VehicleRegistrationAPI.MaxCompetentAuthoritySize)]
        public string CompetentAuthority;
        public Int32 CompetentAuthoritySize;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = VehicleRegistrationAPI.MaxAuthorityIssuingSize)]
        public string AuthorityIssuing;
        public Int32 AuthorityIssuingSize;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = VehicleRegistrationAPI.MaxUnambiguousNumberSize)]
        public string UnambiguousNumber;
        public Int32 UnambiguousNumberSize;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = VehicleRegistrationAPI.MaxIssuingDateSize)]
        public string IssuingDate;
        public Int32 IssuingDateSize;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = VehicleRegistrationAPI.MaxExpiryDateSize)]
        public string ExpiryDate;
        public Int32 ExpiryDateSize;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = VehicleRegistrationAPI.MaxSerialNumberSize)]
        public string SerialNumber;
        public Int32 SerialNumberSize;
    }
}
