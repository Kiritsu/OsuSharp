# OsuSharp

[![NuGet version](https://badge.fury.io/nu/OsuSharp.svg)](https://badge.fury.io/nu/OsuSharp)

**Osu!Sharp is a wrapper written in C# for the Osu!Api.**

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
private const string apiKey = "Your_Osu_API_Key"; //You can create one here: https://osu.ppy.sh/p/api

public async Task GetOsuBeatmapAsync(long beatmapId)
{
    OsuApi.Init(apiKey);
    var api = await OsuSharp.GetBeatmapAsync(1317488);
    Console.WriteLine($"The creator of the map is: {api.Creator}");
}
```
