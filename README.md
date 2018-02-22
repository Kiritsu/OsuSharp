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
PM> Install-Package OsuSharp -Version 1.4.5
```

Or compile from source

```git
git clone https://github.com/Kiritsu/OsuSharp.git
```

## Development

If you want to contribute, feel free to use Issues or Pull Request

## Help

Feel free to join my Discord Server: https://discord.gg/bXKXNAR

## Example

```cs
private const string API_KEY = "Your_Osu_API_Key"; //You can get one here: https://osu.ppy.sh/p/api

public async Task GetOsuBeatmapAsync(ulong beatmapId)
{
    OsuApi.Init(API_KEY);
    var api = await OsuApi.GetBeatmapAsync(beatmapId);
    Console.WriteLine($"The creator of the map is: {api.Creator}");
}
```
