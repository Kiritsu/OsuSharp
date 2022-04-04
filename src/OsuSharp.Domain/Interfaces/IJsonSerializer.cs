using System.IO;

namespace OsuSharp.Interfaces;

/// <summary>
/// Interfaces the JSON serialization for model communication with the osu! api. 
/// </summary>
public interface IJsonSerializer
{
    /// <summary>
    /// Deserializes a string content into a model.
    /// </summary>
    /// <param name="content">JSON content to deserialize.</param>
    /// <typeparam name="T">Model to deserialize the JSON into.</typeparam>
    T? Deserialize<T>(string content) where T : class;

    /// <summary>
    /// Serializes a model as a JSON.
    /// </summary>
    /// <param name="value">Model to serialize to JSON.</param>
    /// <typeparam name="T">Type of the model.</typeparam>
    Stream Serialize<T>(T value) where T : class;
}