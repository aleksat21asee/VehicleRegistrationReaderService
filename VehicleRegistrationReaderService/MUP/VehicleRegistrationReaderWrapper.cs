using System;
using System.Text;
using System.Threading.Tasks;
using VehicleRegistrationReaderService.Models;
using VehicleRegistrationReaderService.Models.Exceptions;

namespace VehicleRegistrationReaderService.MUP
{
    class VehicleRegistrationReaderWrapper : IVehicleRegistrationReaderWrapper
    {
        public VehicleRegistrationReaderWrapper()
        {

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
                throw new VehicleRegistrationReaderException
                {
                    Method = "Select reader",
                    Status = selectReaderStatus,
                    StatusMessage = VehicleRegistrationAPI.ResponseMessage(selectReaderStatus),
                    DisplayMessage = VehicleRegistrationAPI.ResponseMessage(selectReaderStatus)
                };
            }

            var processNewCardStatus = VehicleRegistrationAPI.sdProcessNewCard();
            if (processNewCardStatus != VehicleRegistrationAPI.S_OK)
            {
                throw new VehicleRegistrationReaderException
                {
                    Method = "Process new card",
                    Status = processNewCardStatus,
                    StatusMessage = VehicleRegistrationAPI.ResponseMessage(processNewCardStatus),
                    DisplayMessage = VehicleRegistrationAPI.ResponseMessage(processNewCardStatus)
                };
            }

        }

        public async Task<string> GetPersonalData(string readerName)
        {

            SetupNewCard(readerName);


            return "Dosao sam do ovde";
        }

    }
}
