# OsuSharp

[![NuGet version](https://badge.fury.io/nu/OsuSharp.svg)](https://badge.fury.io/nu/OsuSharp)

**Osu!Sharp is a wrapper written in C# for the Osu!Api.**
- Compatible with .NET Core. 
- Is Asynchrone

## Requirements:
- Newtonsoft.Json (>= 10.0.3)
- Microsoft.NETCore.App (>= 1.0.5)

## Installation
You can install the wrapper with Nuget

```
PM> Install-Package OsuSharp -Version 1.2.0
```

Or compile from source

```git
git clone https://github.com/Kiritsu/OsuSharp.git
```

## Development
If you want to contribute, feel free to use Issues or Pull Request

## Help
Feel free to join my Discord Server: https://discord.gg/bXKXNAR

## Exemple
```cs
private const string apiKey = "Your_Osu_API_Key"; //You can create one here: https://osu.ppy.sh/p/api

public async Task GetOsuBeatmapAsync(long beatmapId)
{
	OsuApi.Init(apiKey);
	var api = await OsuSharp.GetBeatmapAsync(BeatmapType.Difficulty, 1317488);
	Console.WriteLine($"The creator of the map is: {api.Creator}");
}
```

##### Different methods:
-- GetBeatmapAsync(long beatmapId, (enum)BeatmapType bmType);

-- GetBeatmapsAsync(int limit [default = 500]);

-- GetBeatmapsByCreatorAsync(long userId, (enum)BeatmapType bmType, int limit [default = 500]);

-- GetUserByNameAsync(string username, (enum)OsuMode oMode [default = OsuMode.Standard]);

-- GetUserByIdAsync(long userid, (enum)OsuMode oMode [default = OsuMode.Standard]);

-- GetScoreByUsernameAsync(long beatmapid, string username, (enum)OsuMode oMode [default = OsuMode.Standard]);

-- GetScoreByUseridAsync(long beatmapid, long userid, (enum)OsuMode oMode [default = OsuMode.Standard]);

-- GetScoresAsync(long beatmapid, (enum)OsuMode oMode [default = OsuMode.Standard], int limit [default = 10]);

-- GetUserBestByUsernameAsync(string username, (enum)OsuMode oMode [default = OsuMode.Standard]);

-- GetUserBestByUseridAsync(long userid, (enum)OsuMode oMode [default = OsuMode.Standard], int limit [default = 10]);

-- GetUserRecentByUsernameAsync(string username, (enum)OsuMode oMode [default = OsuMode.Standard], int limit [default = 10]);

-- GetUserRecentbByUseridAsync(long userid, (enum)OsuMode oMode [default = OsuMode.Standard], int limit [default = 10]);

-- GetMatchAsync(long matchid);

-- GetReplayByUsernameAsync(long beatmapid, string username, (enum)OsuMode oMode [default = OsuMode.Standard]);

-- GetReplayByUseridAsync(long beatmapid, long userid, (enum)OsuMode oMode [default = OsuMode.Standard]);
