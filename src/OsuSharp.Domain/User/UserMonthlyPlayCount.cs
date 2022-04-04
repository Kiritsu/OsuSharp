using System;
using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public sealed class UserMonthlyPlayCount : IUserMonthlyPlayCount
{
    public DateTimeOffset StartDate { get; internal set; }

    public long Count { get; internal set; }

    internal UserMonthlyPlayCount()
    {
            
    }
}