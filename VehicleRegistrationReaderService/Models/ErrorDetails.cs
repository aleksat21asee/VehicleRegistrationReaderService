using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace VehicleRegistrationReaderService.Models
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }

        public string Method { get; set; }

        public string DisplayMessage { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }


}
