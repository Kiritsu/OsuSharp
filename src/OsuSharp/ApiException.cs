using System;
using System.Net;

namespace OsuSharp
{
    public class ApiException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        
        public ApiException(string reason, HttpStatusCode statusCode) : base(reason)
        {
            StatusCode = statusCode;
        }
    }
}