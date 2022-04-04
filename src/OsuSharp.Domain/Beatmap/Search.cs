using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public class Search : ISearch
{
    public BeatmapSorting Sort { get; internal set; }

    internal Search()
    {

    }
}