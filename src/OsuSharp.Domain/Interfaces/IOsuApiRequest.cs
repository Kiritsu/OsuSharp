using System;
using System.Collections.Generic;
using System.Net.Http;

namespace OsuSharp.Interfaces;

/// <summary>
/// Interfaces an osu api request.
/// </summary>
public interface IOsuApiRequest
{
    /// <summary>
    /// Gets or sets the client responsible of this API request.
    /// </summary>
    IOsuClient Client { get; set; }

    /// <summary>
    /// Gets or sets the token used to make the request.
    /// </summary>
    IOsuToken? Token { get; set; }

    /// <summary>
    /// Gets or sets the kind of http method to make.
    /// </summary>
    HttpMethod Method { get; set; }

    /// <summary>
    /// Gets or sets the endpoint of the request.
    /// </summary>
    string Endpoint { get; set; }

    /// <summary>
    /// Gets or sets the route of the request.
    /// </summary>
    Uri Route { get; set; }

    /// <summary>
    /// Gets or sets the different parameters. Either transformed in query string or for a JSON body.
    /// </summary>
    IDictionary<string, string>? Parameters { get; set; }
}