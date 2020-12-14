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
    }
}