using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleRegistrationReaderService.Models;
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

        [Route("test")]
        public async Task<ActionResult<string>> GetTest()
        {
            return Ok("Service is running succesfully");
        }

        [Route("reader-names")]
        public async Task<ActionResult<CardReaderList>> GetReaderName()
        {
            var readerNames = await _vehicleRegistrationReaderWrapper.GetReaderNames();
            return Ok(readerNames);
        }

        [Route("personal-data")]
        public async Task<ActionResult<string>> GetPersonalData(string readerName)
        {
            var result = await _vehicleRegistrationReaderWrapper.GetPersonalData(readerName);
            return Ok(result);
        }
    }
}
