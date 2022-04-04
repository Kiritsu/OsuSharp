using System;
using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public sealed class UserAccountHistory : IUserAccountHistory
{
    public long Id { get; internal set; }

    public string Type { get; internal set; } = null!;

    public DateTimeOffset TimeStamp { get; internal set; }

    public int Length { get; internal set; }

    internal UserAccountHistory()
    {
            
    }
}