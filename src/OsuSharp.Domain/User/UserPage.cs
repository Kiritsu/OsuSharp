using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public sealed class UserPage : IUserPage
{
    public string Html { get; internal set; } = null!;

    public string Raw { get; internal set; } = null!;

    internal UserPage()
    {
            
    }
}