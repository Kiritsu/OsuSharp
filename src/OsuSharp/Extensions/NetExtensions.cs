using System;
using System.Collections.Generic;
using System.Linq;
using OsuSharp.Domain;

namespace OsuSharp.Extensions;

internal static class NetExtensions
{
    public static string AsQueryString<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
    {
        return $"?{string.Join("&", dictionary.Select(x => $"{x.Key}={x.Value}"))}";
    }

    public static string AsLogString<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
    {
        if (dictionary.Count == 0)
        {
            return "empty";
        }

        return string.Join(" | ", dictionary.Select(x => $"{x.Key}:{x.Value}"));
    }

    public static IDictionary<string, string> Build(this BeatmapsetsLookupBuilder @this)
    {
        var builder = new Dictionary<string, string>();

        if (!string.IsNullOrWhiteSpace(@this.Keywords))
        {
            builder["q"] = @this.Keywords;
        }

        if (@this.General != BeatmapSearchGeneral.None)
        {
            builder["c"] = string.Join('.', Enum.GetValues<BeatmapSearchGeneral>()
                .Where(x => @this.General.HasFlag(x) && x != 0)).ToLower();
        }

        if (@this.GameMode.HasValue)
        {
            builder["m"] = ((int)@this.GameMode.Value).ToString();
        }

        if (@this.Category.HasValue)
        {
            builder["s"] = @this.Category.Value.ToString().ToLower();
        }

        if (@this.Genre.HasValue)
        {
            builder["g"] = ((int)@this.Genre.Value).ToString();
        }

        if (@this.Language.HasValue)
        {
            builder["l"] = ((int)@this.Language.Value).ToString();
        }

        if (@this.Extra != BeatmapSearchExtra.None)
        {
            builder["e"] = string.Join('.', Enum.GetValues<BeatmapSearchExtra>()
                .Where(x => @this.Extra.HasFlag(x) && x != 0)).ToLower();
        }

        if (@this.RankAchieved != BeatmapRank.None)
        {
            builder["r"] = string.Join('.', Enum.GetValues<BeatmapRank>()
                .Where(x => @this.RankAchieved.HasFlag(x) && x != 0)).ToLower();
        }

        if (@this.Played.HasValue)
        {
            builder["played"] = @this.Played.Value.ToString();
        }

        if (@this.Nsfw.HasValue)
        {
            builder["nsfw"] = @this.Nsfw == true ? "1" : "0";
        }

        return builder;
    }
}