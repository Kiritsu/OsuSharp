using System.IO;

namespace OsuSharp.Interfaces
{
    public interface IJsonSerializer
    {
        T Deserialize<T>(string content) where T : class;
        MemoryStream Serialize<T>(T value) where T : class;
    }
}