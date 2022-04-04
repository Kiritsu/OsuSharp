using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public sealed class AccessTokenResponse : IAccessTokenResponse
{
    public string TokenType { get; init; } = null!;

    public long ExpiresIn { get; init; }

    public string AccessToken { get; init; } = null!;

    public string RefreshToken { get; init; } = null!;

    internal AccessTokenResponse()
    {

    }
}