using System;
using System.Collections.Generic;
using System.Net.Http;
using OsuSharp.Interfaces;

namespace OsuSharp.Net
{
    internal sealed class OsuApiRequest : IOsuApiRequest
    {
        public IOsuToken Token { get; set; }

        public HttpMethod Method { get; set; }

        public string Endpoint { get; set; }

        public Uri Route { get; set; }

        public IDictionary<string, string> Parameters { get; set; }

        public IOsuClient Client { get; set; }
    }
}