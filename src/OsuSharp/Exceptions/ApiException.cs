using System;
using System.Net;

namespace OsuSharp.Exceptions
{
    public class ApiException : Exception
    {
        /// <summary>
        ///     Gets the status code of the api request.
        /// </summary>
        public HttpStatusCode StatusCode { get; }
        
        internal ApiException(string reason, HttpStatusCode statusCode) : base(reason)
        {
            StatusCode = statusCode;
        }
    }
}