# OsuSharp 3.2.0 (11-06-2018)

[![NuGet version](https://badge.fury.io/nu/OsuSharp.svg)](https://badge.fury.io/nu/OsuSharp)
[![Build status](https://ci.appveyor.com/api/projects/status/jhrtfqgrhidw331x?svg=true)](https://ci.appveyor.com/project/Kiritsu/osusharp)

**OsuSharp is a wrapper written in C# for the osu! API.**

- Compatible with .NET Standard (2.0)
- Supports both synchronous and asynchronous API requests (through the TAP model)

## Requirements:

- Newtonsoft.Json (>= 10.0.3)

## Installation

You can install the wrapper with NuGet:

```
PM> Install-Package OsuSharp -Version 3.2.0
```

Or compile from source:

```git
git clone https://github.com/Kiritsu/OsuSharp.git
```

## Contributing

If you want to contribute, feel free to use Issues or Pull Requests!

## Documentation

https://kiritsu.github.io/OsuSharp/index.html

## Contact

You can join my personal Discord server: https://discord.gg/mnqsg7q

## Thanks

Thanks to the following contributors: 
- [NaamloosDT](https://github.com/NaamloosDT)
- [Kamdzy](https://github.com/Kamdzy)
- [Bond-009](https://github.com/Bond-009)
- [POP/STARS](https://github.com/popstars)

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

You can also check out the [OsuSharp.Example](https://github.com/Kiritsu/OsuSharp/tree/master/src/OsuSharp.Example) project for a more fully-fledged example.
