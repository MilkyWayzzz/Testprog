using System;
using System.Collections.Generic;
using System.Text;

namespace TestProg.ConsoleClient.Exceptions
{
    public class HttpServiceException : Exception
    {
        public HttpServiceException()
        {
        }

        public HttpServiceException(string message) : base(message)
        {
        }
    }
}
