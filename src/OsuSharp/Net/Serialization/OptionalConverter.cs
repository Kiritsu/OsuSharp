using System;
using System.Reflection;
using Newtonsoft.Json;

namespace OsuSharp.Net.Serialization
{
    internal sealed class OptionalConverter : JsonConverter
    {
        public static readonly OptionalConverter Instance = new();

        private OptionalConverter()
        {
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var type = value.GetType();
            var hasValue = type.GetProperty("HasValue", BindingFlags.Public)!.GetValue(value);
            if (!(bool) hasValue!)
                writer.WriteNull();
            else
                serializer.Serialize(writer, type.GetField("_value", BindingFlags.NonPublic)!.GetValue(value));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            var field = objectType.GetField("Default", BindingFlags.Static | BindingFlags.NonPublic);
            if (reader.Value is null)
            {
                return field!.GetValue(null);
            }
            
            return objectType.GetConstructors()[0]
                .Invoke(new[] {serializer.Deserialize(reader, objectType.GenericTypeArguments[0])});
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }
    }
}