namespace OsuSharp.Interfaces;

/// <summary>
/// Defines an entity that holds the osu client.
/// </summary>
public interface IClientEntity
{
    /// <summary>
    /// Instance of the client that made the request resulting in this entity.
    /// </summary>
    IOsuClient Client { get; }
}