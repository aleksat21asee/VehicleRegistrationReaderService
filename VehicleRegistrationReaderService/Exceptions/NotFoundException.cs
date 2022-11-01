﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleRegistrationReaderService.Exceptions
{
    public abstract class NotFoundException : Exception
    {
        public NotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
        public NotFoundException(string message)
            : base(message)
        {
        }
    }
}
