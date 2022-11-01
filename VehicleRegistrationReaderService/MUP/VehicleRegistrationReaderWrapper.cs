using AutoMapper;
using System;
using System.Text;
using System.Threading.Tasks;
using VehicleRegistrationReaderService.Exceptions;
using VehicleRegistrationReaderService.Exceptions.DomainExceptions;
using VehicleRegistrationReaderService.Models;
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

    }
}
