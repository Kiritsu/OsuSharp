using System;
using System.Collections.Generic;
using System.Net.Http;

namespace OsuSharp.Net
{
    internal sealed class OsuApiRequest
    {
        public HttpMethod Method { get; set; }

        public string Endpoint { get; set; }

        public Uri Route { get; set; }

        public IDictionary<string, string> Parameters { get; set; }
    }
}