using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleRegistrationReaderService.Models.ResponseClasses
{
    public class PersonalData
    {
        public string OwnersPersonalNo { get; set; }

        public string OwnersSurnameOrBusinessName { get; set; }

        public string OwnerName { get; set; }

        public string OwnerAddress { get; set; }

        public string UsersPersonalNo { get; set; }

        public string UsersSurnameOrBusinessName { get; set; }

        public string UsersName { get; set; }

        public string UsersAddress { get; set; }
    }
}
