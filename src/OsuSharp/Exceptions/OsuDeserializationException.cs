using System;

namespace OsuSharp.Exceptions;

/// <summary>
/// Represents an exception that occured when deserializing a JSON payload.
/// </summary>
public sealed class OsuDeserializationException : Exception
{
    /// <summary>
    /// Gets the type that was used for serialization.
    /// </summary>
    public Type TargetType { get; }

    /// <summary>
    /// Gets the json payload that was returned by the api.
    /// </summary>
    public string JsonPayload { get; }

    internal OsuDeserializationException(Type targetType, Exception? innerException, string jsonPayload)
        : base("A deserialization error occured. See the given payload and inner exception for more details.",
            innerException)
    {
        TargetType = targetType;
        JsonPayload = jsonPayload;
    }
}