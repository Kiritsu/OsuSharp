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

        /// <summary>
        ///     Gets the json payload that was returned by the api.
        /// </summary>
        public string JsonPayload { get; }

        internal ApiException(string reason, HttpStatusCode statusCode, string jsonPayload) : base(reason)
        {
            StatusCode = statusCode;
            JsonPayload = jsonPayload;
        }
    }
}