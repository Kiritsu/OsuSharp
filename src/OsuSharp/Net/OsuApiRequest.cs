using System;
using System.Collections.Generic;
using System.Net.Http;
using OsuSharp.Interfaces;

namespace OsuSharp.Net;

internal sealed class OsuApiRequest : IOsuApiRequest
{
    public IOsuToken? Token { get; set; }

    public HttpMethod Method { get; set; } = HttpMethod.Get;

    public string Endpoint { get; set; } = null!;

    public Uri Route { get; set; } = null!;

    public IDictionary<string, string>? Parameters { get; set; }

    public IOsuClient Client { get; set; } = null!;
}