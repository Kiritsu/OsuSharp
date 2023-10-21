using Microsoft.Extensions.Options;
using OsuSharp.Interfaces;

namespace OsuSharp.Models;

/// <summary>
/// Represents the different options for the OsuSharp environment.
/// </summary>
public class OsuSharpOptions : IOptions<OsuSharpOptions>
{
    /// <summary>
    /// Gets the name of the options.
    /// </summary>
    public const string Name = "OsuSharpOptions";
            
    /// <summary>
    /// Gets or sets the instance of the client configuration.
    /// </summary>
    public IOsuClientConfiguration? Configuration { get; set; }

    /// <summary>
    /// Gets or sets the instance of the JSON serializer.
    /// </summary>
    public IJsonSerializer? JsonSerializer { get; set; }

    /// <summary>
    /// Gets or sets the instance of the request handler.
    /// </summary>
    public IRequestHandler? RequestHandler { get; set; }
        
    /// <summary>
    /// Gets or sets whether to use scoped services or not.
    /// </summary>
    public bool UseScopedServices { get; set; }

    /// <summary>
    /// Gets the instance of the current options.
    /// </summary>
    public OsuSharpOptions Value => this;
}