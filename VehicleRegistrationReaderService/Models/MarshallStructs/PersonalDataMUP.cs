using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using VehicleRegistrationReaderService.MUP;

namespace VehicleRegistrationReaderService.Models
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct PersonalDataMUP
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = VehicleRegistrationAPI.MaxOwnersPersonalNoSize)]
        public string OwnersPersonalNo;
        public Int32 OwnersPersonalNoSize;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = VehicleRegistrationAPI.MaxOwnersSurnameOrBusinessNameSize)]
        public string OwnersSurnameOrBusinessName;
        public Int32 OwnersSurnameOrBusinessNameSize;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = VehicleRegistrationAPI.MaxOwnerNameSize)]
        public string OwnerName;
        public Int32 OwnerNameSize;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = VehicleRegistrationAPI.MaxOwnerAddressSize)]
        public string OwnerAddress;
        public Int32 OwnerAddressSize;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = VehicleRegistrationAPI.MaxUsersPersonalNoSize)]
        public string UsersPersonalNo;
        public Int32 UsersPersonalNoSize;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = VehicleRegistrationAPI.MaxusersSurnameOrBusinessNameSize)]
        public string UsersSurnameOrBusinessName;
        public Int32 UsersSurnameOrBusinessNameSize;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = VehicleRegistrationAPI.MaxUsersNameSize)]
        public string UsersName;
        public Int32 UsersNameSize;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = VehicleRegistrationAPI.MaxUsersAddressSize)]
        public string UsersAddress;
        public Int32 UsersAddressSize;
    }
}
