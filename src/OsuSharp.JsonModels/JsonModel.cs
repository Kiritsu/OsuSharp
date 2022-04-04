using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OsuSharp.JsonModels;

public class JsonModel
{
    [JsonExtensionData] 
    public IDictionary<string, JToken> ExtensionData { get; set; } = null!;
}