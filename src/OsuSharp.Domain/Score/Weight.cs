using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public sealed class Weight : IWeight
{
    public double Percentage { get; internal set; }

    public double PerformancePoints { get; internal set; }

    internal Weight()
    {
            
    }
}