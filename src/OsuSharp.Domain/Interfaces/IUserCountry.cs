using System.Globalization;

namespace OsuSharp.Interfaces;

public interface IUserCountry
{
    string Code { get; }
    string Name { get; }
    RegionInfo RegionInfo { get; }
}