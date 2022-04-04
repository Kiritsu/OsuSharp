using OsuSharp.Domain;

namespace OsuSharp.Interfaces;

public interface ISearch
{
    BeatmapSorting Sort { get; }
}