using System.Globalization;
using OsuSharp.Interfaces;

namespace OsuSharp.Domain
{
    public sealed class UserCountry : IUserCountry
    {
        public string Code { get; internal set; }

        public string Name { get; internal set; }

        public RegionInfo RegionInfo => _regionInfo ??= new RegionInfo(Name);
        private RegionInfo _regionInfo;

        internal UserCountry()
        {
            
        }
    }
}