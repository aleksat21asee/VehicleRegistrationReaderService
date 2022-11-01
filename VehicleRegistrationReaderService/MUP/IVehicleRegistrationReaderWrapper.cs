using System.Threading.Tasks;
using VehicleRegistrationReaderService.Models;
using VehicleRegistrationReaderService.Models.ResponseClasses;

namespace VehicleRegistrationReaderService.MUP
{
    public interface IVehicleRegistrationReaderWrapper
    {
        Task<CardReaderList> GetReaderNames();

        Task<PersonalData> GetPersonalData(string readerName);
    }
}
