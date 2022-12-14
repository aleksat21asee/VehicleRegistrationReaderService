using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleRegistrationReaderService.Models;
using VehicleRegistrationReaderService.Models.MarshallStructs;
using VehicleRegistrationReaderService.Models.ResponseClasses;

namespace VehicleRegistrationReaderService.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<PersonalDataMUP, PersonalDataResponse>();
            CreateMap<RegistrationDataMUP, RegistrationDataResponse>();
            CreateMap<DocumentDataMUP, DocumentDataResponse>();
            CreateMap<VehicleDataMUP, VehicleDataResponse>();
        }
    }
}
