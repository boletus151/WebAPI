using System;
using System.Collections.Generic;
using System.Text;

namespace WebAPI_BaseComponents.Exceptions
{
    public class InvalidBackendCastException : Exception
    {
        public InvalidBackendCastException(string response, string endpoint,
            string message = "Invalid backend cast exception", Exception innerException = null) : base(message, innerException)
        {
            this.ServiceResponse = response;
            this.Endpoint = endpoint;
            this.CustomMessage = "There was an error parsing the response comming from the backend";
        }

        public string ServiceResponse { get; }

        public string Endpoint { get; set; }

        public string CustomMessage { get; set; }
    }
}
