using System;

namespace OsuSharp.Exceptions;

/// <summary>
/// Represents an exception that occured when serializing an object to JSON.
/// </summary>
public sealed class OsuSerializationException : Exception
{
    /// <summary>
    /// Gets the type that was used for serialization.
    /// </summary>
    public Type TargetType { get; }

    /// <summary>
    /// Gets the json payload that was returned by the api.
    /// </summary>
    public object Object { get; }

    internal OsuSerializationException(Type targetType, Exception innerException, object @object)
        : base("A serialization error occured. See the given payload and inner exception for more details.",
            innerException)
    {
        TargetType = targetType;
        Object = @object;
    }
}