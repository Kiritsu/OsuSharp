using System.Globalization;
using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public sealed class UserCountry : IUserCountry
{
    public string Code { get; internal set; } = null!;

    public string Name { get; internal set; } = null!;

    public RegionInfo RegionInfo => _regionInfo ??= new RegionInfo(Code);
    private RegionInfo? _regionInfo;

    internal UserCountry()
    {
            
    }
}