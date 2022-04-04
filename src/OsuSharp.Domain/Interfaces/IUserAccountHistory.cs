using System;

namespace OsuSharp.Interfaces;

public interface IUserAccountHistory
{
    long Id { get; }
    string Type { get; }
    DateTimeOffset TimeStamp { get; }
    int Length { get; }
}