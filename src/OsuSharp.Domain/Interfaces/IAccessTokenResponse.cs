namespace OsuSharp.Interfaces
{
    public interface IAccessTokenResponse
    {
        string TokenType { get; }
        long ExpiresIn { get; }
        string AccessToken { get; }
        string RefreshToken { get; }
    }
}