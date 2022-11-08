using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleRegistrationReaderService.Models;
using VehicleRegistrationReaderService.Models.ResponseClasses;
using VehicleRegistrationReaderService.MUP;

namespace VehicleRegistrationReaderService.Controllers
{
    [Route("v1/vehicle-registration-card-reader")]
    public class VehicleRegistrationReaderController : Controller
    {
        private readonly ILogger<VehicleRegistrationReaderController> _logger;
        private readonly IVehicleRegistrationReaderWrapper _vehicleRegistrationReaderWrapper;

        public VehicleRegistrationReaderController(
            ILogger<VehicleRegistrationReaderController> logger,
            IVehicleRegistrationReaderWrapper vehicleRegistrationReaderWrapper)
        {
            _logger = logger;
            _vehicleRegistrationReaderWrapper = vehicleRegistrationReaderWrapper;
        }

        [Route("reader-names")]
        public async Task<ActionResult<CardReaderList>> GetReaderName()
        {
            var readerNames = await _vehicleRegistrationReaderWrapper.GetReaderNames();
            return Ok(readerNames);
        }

        [Route("personal-data")]
        public async Task<ActionResult<PersonalDataResponse>> GetPersonalData(string readerName)
        {
            var result = await _vehicleRegistrationReaderWrapper.GetPersonalData(readerName);
            return Ok(result);
        }

        [Route("document-data")]
        public async Task<ActionResult<DocumentDataResponse>> GetDocumentData(string readerName)
        {
            var result = await _vehicleRegistrationReaderWrapper.GetDocumentData(readerName);
            return Ok(result);
        }

        [Route("vehicle-data")]
        public async Task<ActionResult<VehicleDataResponse>> GetVehicleData(string readerName)
        {
            var result = await _vehicleRegistrationReaderWrapper.GetVehicleData(readerName);
            return Ok(result);
        }

        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> RefreshService()
        {
            await _vehicleRegistrationReaderWrapper.RefreshService();
            return Ok();
        }
    }
}
