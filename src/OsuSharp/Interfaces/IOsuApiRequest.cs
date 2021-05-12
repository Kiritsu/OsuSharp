using System;
using System.Collections.Generic;
using System.Net.Http;

namespace OsuSharp.Interfaces
{
    public interface IOsuApiRequest
    {
        HttpMethod Method { get; set; }
        string Endpoint { get; set; }
        Uri Route { get; set; }
        IDictionary<string, string> Parameters { get; set; }
        
    }
}