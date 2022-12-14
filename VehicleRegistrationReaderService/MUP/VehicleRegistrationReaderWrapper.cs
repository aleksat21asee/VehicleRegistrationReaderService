using AutoMapper;
using System;
using System.Text;
using System.Threading.Tasks;
using VehicleRegistrationReaderService.Exceptions;
using VehicleRegistrationReaderService.Models;
using VehicleRegistrationReaderService.Models.MarshallStructs;
using VehicleRegistrationReaderService.Models.ResponseClasses;

namespace VehicleRegistrationReaderService.MUP
{
    class VehicleRegistrationReaderWrapper : IVehicleRegistrationReaderWrapper
    {
        private readonly IMapper _mapper;
        public VehicleRegistrationReaderWrapper(IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CardReaderList> GetReaderNames()
        {

            UInt32 cardReaderIndex = 0;

            Int32 bufferSize = VehicleRegistrationAPI.MUP_READER_LENGTH_SIZE;
            StringBuilder readerName = new StringBuilder(bufferSize);

            var cardReaderList = new CardReaderList();

            while ((VehicleRegistrationAPI.GetReaderName(cardReaderIndex++, readerName, ref bufferSize)) == VehicleRegistrationAPI.S_OK)
            {
                cardReaderList.CardReaders.Add(new CardReader { ReaderName = readerName.ToString() });
            }

            return cardReaderList;

        }

        private void SetupNewCard(string readerName)
        {
            StringBuilder sbReaderName = new StringBuilder(readerName);

            var selectReaderStatus = VehicleRegistrationAPI.SelectReader(sbReaderName);
            if (selectReaderStatus != VehicleRegistrationAPI.S_OK)
            {
                throw new WrapperException("SelectReader", new NotFoundException(VehicleRegistrationAPI.ResponseMessage(selectReaderStatus)));
            }

            var processNewCardStatus = VehicleRegistrationAPI.sdProcessNewCard();
            if (processNewCardStatus != VehicleRegistrationAPI.S_OK)
            {
                throw new WrapperException("sdProcessNewCard", new BadRequestException(VehicleRegistrationAPI.ResponseMessage(processNewCardStatus)));

            }

        }

        public async Task<PersonalDataResponse> GetPersonalData(string readerName)
        {
            SetupNewCard(readerName);

            PersonalDataMUP personalDataMUP = new PersonalDataMUP();

            var getPersonalDataStatus = VehicleRegistrationAPI.sdReadPersonalData(ref personalDataMUP);

            if (getPersonalDataStatus != VehicleRegistrationAPI.S_OK)
            {
                throw new WrapperException("sdReadPersonalData", new BadRequestException(VehicleRegistrationAPI.ResponseMessage(getPersonalDataStatus)));
            }

            return _mapper.Map<PersonalDataResponse>(personalDataMUP);
        }

        public async Task<DocumentDataResponse> GetDocumentData(string readerName)
        {
            SetupNewCard(readerName);

            DocumentDataMUP documentDataMUP = new DocumentDataMUP();

            var getDocumentDataStatus = VehicleRegistrationAPI.sdReadDocumentData(ref documentDataMUP);

            if (getDocumentDataStatus != VehicleRegistrationAPI.S_OK)
            {
                throw new WrapperException("sdReadDocumentData", new BadRequestException(VehicleRegistrationAPI.ResponseMessage(getDocumentDataStatus)));
            }

            return _mapper.Map<DocumentDataResponse>(documentDataMUP);
        }

        public async Task<VehicleDataResponse> GetVehicleData(string readerName)
        {
            SetupNewCard(readerName);

            VehicleDataMUP vehicleDataMUP = new VehicleDataMUP();

            var getVehicleDataStatus = VehicleRegistrationAPI.sdReadVehicleData(ref vehicleDataMUP);

            if (getVehicleDataStatus != VehicleRegistrationAPI.S_OK)
            {
                throw new WrapperException("sdReadVehicleData", new BadRequestException(VehicleRegistrationAPI.ResponseMessage(getVehicleDataStatus)));
            }

            return _mapper.Map<VehicleDataResponse>(vehicleDataMUP);
        }

        // NOT USED
        private RegistrationDataResponse GetRegistrationData(string readerName)
        {
            SetupNewCard(readerName);

            RegistrationDataMUP registrationDataMUP = new RegistrationDataMUP();

            var getRegistrationDataStatus = VehicleRegistrationAPI.sdReadRegistration(ref registrationDataMUP, 1);

            if (getRegistrationDataStatus != VehicleRegistrationAPI.S_OK)
            {
                throw new WrapperException("sdReadRegistration", new BadRequestException(VehicleRegistrationAPI.ResponseMessage(getRegistrationDataStatus)));
            }

            return _mapper.Map<RegistrationDataResponse>(registrationDataMUP);
        }

        public async Task RefreshService()
        {
            var status = VehicleRegistrationAPI.sdCleanup();

            status = VehicleRegistrationAPI.sdStartup(1);
            if (status != VehicleRegistrationAPI.S_OK)
            {
                throw new WrapperException("sdStartup", new ServiceException(VehicleRegistrationAPI.ResponseMessage(status)));
            }

        }

        public async Task<CombinedData> GetCombinedData(string readerName)
        {
            SetupNewCard(readerName);

            /* Vehicle data */
            VehicleDataMUP vehicleDataMUP = new VehicleDataMUP();

            var getVehicleDataStatus = VehicleRegistrationAPI.sdReadVehicleData(ref vehicleDataMUP);

            if (getVehicleDataStatus != VehicleRegistrationAPI.S_OK)
            {
                throw new WrapperException("sdReadVehicleData", new BadRequestException(VehicleRegistrationAPI.ResponseMessage(getVehicleDataStatus)));
            }

            var vehicleDataResponse = _mapper.Map<VehicleDataResponse>(vehicleDataMUP);

            /* Document data */
            DocumentDataMUP documentDataMUP = new DocumentDataMUP();

            var getDocumentDataStatus = VehicleRegistrationAPI.sdReadDocumentData(ref documentDataMUP);

            if (getDocumentDataStatus != VehicleRegistrationAPI.S_OK)
            {
                throw new WrapperException("sdReadDocumentData", new BadRequestException(VehicleRegistrationAPI.ResponseMessage(getDocumentDataStatus)));
            }

            var documentDataResponse = _mapper.Map<DocumentDataResponse>(documentDataMUP);


            /* Personal data */
            PersonalDataMUP personalDataMUP = new PersonalDataMUP();

            var getPersonalDataStatus = VehicleRegistrationAPI.sdReadPersonalData(ref personalDataMUP);

            if (getPersonalDataStatus != VehicleRegistrationAPI.S_OK)
            {
                throw new WrapperException("sdReadPersonalData", new BadRequestException(VehicleRegistrationAPI.ResponseMessage(getPersonalDataStatus)));
            }

            var personalDataResponse = _mapper.Map<PersonalDataResponse>(personalDataMUP);

            /* Combine results */
            return new CombinedData()
            {
                VehicleData = vehicleDataResponse,
                PersonalData = personalDataResponse,
                DocumentData = documentDataResponse
            };
        }
    }
}
