using System;
using OsuSharp.Domain;

namespace OsuSharp.Interfaces;

public interface IKudosuHistory : IClientEntity
{
    long Id { get; }
    KudosuAction Action { get; }
    long Amount { get; }
    string Model { get; }
    DateTimeOffset CreatedAt { get; }
    IKudosuGiver Giver { get; }
    IKudosuPost Post { get; }
}