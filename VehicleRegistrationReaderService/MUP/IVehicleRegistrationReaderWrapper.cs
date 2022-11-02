using System.Threading.Tasks;
using VehicleRegistrationReaderService.Models;
using VehicleRegistrationReaderService.Models.ResponseClasses;

namespace VehicleRegistrationReaderService.MUP
{
    public interface IVehicleRegistrationReaderWrapper
    {
        Task<CardReaderList> GetReaderNames();

        Task<PersonalDataResponse> GetPersonalData(string readerName);

        Task<RegistrationDataResponse> GetRegistrationData(string readerName);

        Task<DocumentDataResponse> GetDocumentData(string readerName);

        Task<VehicleDataResponse> GetVehicleData(string readerName);
    }
}
