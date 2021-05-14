using Microsoft.Extensions.Options;
using OsuSharp.Interfaces;

namespace OsuSharp.Models
{
    public class OsuSharpOptions : IOptions<OsuSharpOptions>
    {
        public const string Name = "OsuSharpOptions";
            
        public IOsuClientConfiguration Configuration { get; set; }
        public IJsonSerializer JsonSerializer { get; set; }
        public IRequestHandler RequestHandler { get; set; }
        
        public OsuSharpOptions Value => this;
    }
}