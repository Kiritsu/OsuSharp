# OsuSharp 3.1.3 (10-01-2018)

[![NuGet version](https://badge.fury.io/nu/OsuSharp.svg)](https://badge.fury.io/nu/OsuSharp)
[![Build status](https://ci.appveyor.com/api/projects/status/jhrtfqgrhidw331x?svg=true)](https://ci.appveyor.com/project/Kiritsu/osusharp)

**OsuSharp is a wrapper written in C# for the Osu!Api.**

- Compatible with .NET Standard (2.0)
- Is synchrone and asynchrone

## Requirements:

- Newtonsoft.Json (>= 10.0.3)

## Installation

You can install the wrapper with Nuget

```
PM> Install-Package OsuSharp -Version 3.1.3
```

Or compile from source

```git
git clone https://github.com/Kiritsu/OsuSharp.git
```

## Development or help

If you want to contribute, feel free to use Issues or Pull Requests

## Docs

https://kiritsu.github.io/OsuSharp/index.html

## Contact

You can join my personal Discord server: https://discord.gg/mnqsg7q

## Thanks

Thanks to the contributors: [NaamloosDT](https://github.com/NaamloosDT), [Kamdzy](https://github.com/Kamdzy), [Bond-009](https://github.com/Bond-009)

## Example

```cs
//I DO NOT SUGGEST YOU TO WRITE YOUR API KEY, PLEASE PARSE IT FROM AN EXTERNAL FILE.
private const string API_KEY = "Your_Osu_API_Key"; //You can get one here: https://osu.ppy.sh/p/api
private IOsuApi Instance { get; }

public MyClass() 
{
    Instance = new OsuApi(new OsuSharpConfiguration
    {
        ApiKey = API_KEY,
        ModsSeparator = "|",
        LogLevel = LoggingLevel.Debug
    });
    
    Instance.Logger.LogMessageReceived += (sender, args) =>
        args.Logger.Print(args.Level, args.From, args.Message, args.Time);
    
    [...]
}

public async Task GetOsuBeatmapAsync(ulong beatmapId)
{
    var beatmap = await Instance.GetBeatmapAsync(beatmapId);
    Console.WriteLine($"The creator of the map is: {beatmap.Creator}");
}

public void GetOsuBeatmap(ulong beatmapId)
{
    var beatmap = Instance.GetBeatmap(beatmapId);
    Console.WriteLine($"The creator of the map is: {beatmap.Creator}");
}
```

You can also check OsuSharp.Example project.
