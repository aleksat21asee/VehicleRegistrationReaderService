using System;
using System.Text;
using System.Threading.Tasks;
using VehicleRegistrationReaderService.Models;

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
    }
}
