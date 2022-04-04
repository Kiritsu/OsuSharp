using System;

namespace OsuSharp.Interfaces;

public interface IUserMonthlyPlayCount
{
    DateTimeOffset StartDate { get; }
    long Count { get; }
}