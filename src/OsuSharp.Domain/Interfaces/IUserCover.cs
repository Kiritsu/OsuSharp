using System;

namespace OsuSharp.Interfaces;

public interface IUserCover
{
    string CustomUrl { get; }
    Uri Url { get; }
    string Id { get; }
}