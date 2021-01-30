using System.Collections.Generic;
using System.Linq;

namespace OsuSharp.Extensions
{
    internal static class NetExtensions
    {
        public static string AsQueryString<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            return $"?{string.Join("&", dictionary.Select(x => $"{x.Key}={x.Value}"))}";
        }

        public static string ToLogString<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            if (dictionary is null || dictionary.Count == 0)
            {
                return "empty";
            }

            return string.Join(" | ", dictionary.Select(x => $"{x.Key}:{x.Value}"));
        }
    }
}