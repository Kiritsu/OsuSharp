using System;
using System.IO;
using Newtonsoft.Json;
using OsuSharp.Exceptions;
using OsuSharp.Interfaces;

namespace OsuSharp.Net.Serialization;

internal sealed class DefaultJsonSerializer : IJsonSerializer
{
    public static readonly IJsonSerializer Instance = new DefaultJsonSerializer();

    private readonly JsonSerializer _serializer;

    private DefaultJsonSerializer()
    {
        _serializer = new JsonSerializer
        {
            ContractResolver = ContractResolver.Instance
        };
    }

    public T? Deserialize<T>(string content) where T : class
    {
        try
        {
            using var textReader = new StringReader(content);
            using var jsonReader = new JsonTextReader(textReader);
            return _serializer.Deserialize<T>(jsonReader);
        }
        catch (Exception ex)
        {
            throw new OsuDeserializationException(typeof(T), ex, content);
        }
    }

    public Stream Serialize<T>(T value) where T : class
    {
        try
        {
            using var memoryStream = new MemoryStream();
            using var streamWriter = new StreamWriter(memoryStream);
            using var jsonWriter = new JsonTextWriter(streamWriter);
            _serializer.Serialize(jsonWriter, value);
            memoryStream.Seek(0, SeekOrigin.Begin);
            return memoryStream;
        }
        catch (Exception ex)
        {
            throw new OsuSerializationException(typeof(T), ex, value);
        }
    }
}