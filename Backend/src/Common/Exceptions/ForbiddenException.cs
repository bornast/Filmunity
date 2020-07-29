﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException() : base()
        {
        }
        public ForbiddenException(string message) : base(message)
        {
        }
    }
}
