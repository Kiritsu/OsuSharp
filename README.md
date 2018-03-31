# OsuSharp

[![NuGet version](https://badge.fury.io/nu/OsuSharp.svg)](https://badge.fury.io/nu/OsuSharp)

**OsuSharp is a wrapper written in C# for the Osu!Api.**

- Compatible with .NET Standard
- Is Asynchrone

## Requirements:

- Newtonsoft.Json (>= 10.0.3)

## Installation

You can install the wrapper with Nuget

```
PM> Install-Package OsuSharp -Version 1.6.0
```

Or compile from source

```git
git clone https://github.com/Kiritsu/OsuSharp.git
```

## Development or help

If you want to contribute, feel free to use Issues or Pull Request

## Example

```cs
private const string API_KEY = "Your_Osu_API_Key"; //You can get one here: https://osu.ppy.sh/p/api

public async Task GetOsuBeatmapAsync(ulong beatmapId)
{
    OsuApi instance = new OsuApi(API_KEY, "|");
    var beatmap = await instance.GetBeatmapAsync(beatmapId);
    Console.WriteLine($"The creator of the map is: {beatmap.Creator}");
}
```

You can also check OsuSharp.Example project.
