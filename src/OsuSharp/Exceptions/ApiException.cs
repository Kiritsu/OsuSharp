using System;
using System.Net;

namespace OsuSharp.Exceptions;

/// <summary>
/// Represents an exception that occured when trying to retrieve data from the API.
/// </summary>
public sealed class ApiException : Exception
{
    /// <summary>
    /// Gets the status code of the API request.
    /// </summary>
    public HttpStatusCode StatusCode { get; }

    /// <summary>
    /// Gets the json payload that was returned by the API.
    /// </summary>
    public string JsonPayload { get; }

    internal ApiException(string? reason, HttpStatusCode statusCode, string jsonPayload) : base(reason)
    {
        StatusCode = statusCode;
        JsonPayload = jsonPayload;
    }
}