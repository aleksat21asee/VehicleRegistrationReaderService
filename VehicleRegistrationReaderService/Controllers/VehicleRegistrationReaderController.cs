using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleRegistrationReaderService.Controllers
{
    [Route("v1/vehicle-registration-card-reader")]
    public class VehicleRegistrationReaderController : Controller
    {
        private readonly ILogger<VehicleRegistrationReaderController> _logger;
        

        [Route("test")]
        public async Task<ActionResult<string>> GetTest()
        {
            return  Ok("works");
        }
    }
}
