using System.Threading.Tasks;
using VehicleRegistrationReaderService.Models;

namespace VehicleRegistrationReaderService.MUP
{
    public interface IVehicleRegistrationReaderWrapper
    {
        Task<CardReaderList> GetReaderNames();

        Task<string> GetPersonalData(string readerName);
    }
}
