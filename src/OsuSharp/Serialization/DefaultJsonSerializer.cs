using System.IO;
using Newtonsoft.Json;

namespace OsuSharp.Serialization
{
    internal sealed class DefaultJsonSerializer 
    {
        public static readonly DefaultJsonSerializer Instance = new DefaultJsonSerializer();
        
        private readonly JsonSerializer _serializer;

        private DefaultJsonSerializer()
        {
            _serializer = new JsonSerializer
            {
                ContractResolver = ContractResolver.Instance
            };
        }

        public T Deserialize<T>(Stream jsonStream) where T : class
        {
            jsonStream.Seek(0, SeekOrigin.Begin);
            using var textReader = new StreamReader(jsonStream);
            using var jsonReader = new JsonTextReader(textReader);
            return _serializer.Deserialize<T>(jsonReader);
        }

        public MemoryStream Serialize<T>(T value) where T : class
        {
            using var memoryStream = new MemoryStream();
            using var streamWriter = new StreamWriter(memoryStream);
            using var jsonWriter = new JsonTextWriter(streamWriter);
            _serializer.Serialize(jsonWriter, value);
            memoryStream.Seek(0, SeekOrigin.Begin);
            return memoryStream;
        }
    }
}