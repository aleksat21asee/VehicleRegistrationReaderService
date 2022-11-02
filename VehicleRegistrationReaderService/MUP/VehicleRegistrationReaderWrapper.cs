using AutoMapper;
using System;
using System.Text;
using System.Threading.Tasks;
using VehicleRegistrationReaderService.Exceptions;
using VehicleRegistrationReaderService.Exceptions.DomainExceptions;
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

            while ((VehicleRegistrationAPI.GetReaderName(cardReaderIndex++, readerName, ref bufferSize)) != VehicleRegistrationAPI.SCARD_E_UNKNOWN_READER)
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
                throw new WrapperException("SelectReader", new ReaderNotFoundException(readerName));
            }

            var processNewCardStatus = VehicleRegistrationAPI.sdProcessNewCard();
            if (processNewCardStatus != VehicleRegistrationAPI.S_OK)
            {
                throw new WrapperException("sdProcessNewCard", new SetupNewCardException());

            }

        }

        public async Task<PersonalData> GetPersonalData(string readerName)
        {
            SetupNewCard(readerName);

            PersonalDataMUP personalDataMUP = new PersonalDataMUP();

            var getPersonalDataStatus = VehicleRegistrationAPI.sdReadPersonalData(ref personalDataMUP);

            if (getPersonalDataStatus != VehicleRegistrationAPI.S_OK)
            {
                throw new WrapperException("sdReadPersonalData", new SetupNewCardException());
            }

            return _mapper.Map<PersonalData>(personalDataMUP);
        }

        public async Task<RegistrationDataResponse> GetRegistrationData(string readerName)
        {
            SetupNewCard(readerName);

            RegistrationDataMUP registrationDataMUP = new RegistrationDataMUP();

            var getRegistrationDataStatus = VehicleRegistrationAPI.sdReadRegistration(ref registrationDataMUP, 1);

            if (getRegistrationDataStatus != VehicleRegistrationAPI.S_OK)
            {
                throw new WrapperException("sdReadRegistration", new SetupNewCardException());
            }

            return _mapper.Map<RegistrationDataResponse>(registrationDataMUP);
        }

        public async Task<DocumentDataResponse> GetDocumentData(string readerName)
        {
            SetupNewCard(readerName);

            DocumentDataMUP documentDataMUP = new DocumentDataMUP();

            var getDocumentDataStatus = VehicleRegistrationAPI.sdReadDocumentData(ref documentDataMUP);

            if (getDocumentDataStatus != VehicleRegistrationAPI.S_OK)
            {
                throw new WrapperException("sdReadDocumentData", new SetupNewCardException());
            }

            return _mapper.Map<DocumentDataResponse>(documentDataMUP);
        }
    }
}
