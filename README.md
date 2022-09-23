# OsuSharp

[![NuGet version](https://badge.fury.io/nu/OsuSharp.svg)](https://badge.fury.io/nu/OsuSharp)
[![SlimGet](https://img.shields.io/badge/dynamic/json.svg?color=blue&label=slimget&query=%24.items%5B-1%3A%5D.upper&url=https%3A%2F%2Fnuget.emzi0767.com%2Fapi%2Fv3%2Fregistration%2FSemVer2%2Fosusharp%2Findex.json)](https://nuget.emzi0767.com/gallery/search?q=OsuSharp&pre=true)
[![Build Status](https://dev.azure.com/allanmercou/OsuSharp/_apis/build/status/Kiritsu.OsuSharp?branchName=feature%2Fapi-v2)](https://dev.azure.com/allanmercou/OsuSharp/_build/latest?definitionId=11&branchName=feature%2Fapi-v2)

**OsuSharp is a wrapper written in C# for the osu! API.**

- Compatible with .NET 5

## Installation

You can install the stable version of the wrapper with NuGet (only supports API v1 for packages prior to 6.0.0):

```
PM> Install-Package OsuSharp
```

You can also try Nightly Builds for the API v2. You need to add the following URL as a new NuGet Source:

```
​https://nuget.emzi0767.com/api/v3/index.json
```

Then, you can just pull the nightly packages. You may need to enable preview builds to find them.

Finally, you can just compile from source:

```git
git clone https://github.com/Kiritsu/OsuSharp.git
```

## Basic Usage

You can use the following example to get started with the library:

> Program.cs

```cs
public static class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureOsuSharp((ctx, options) => options.Configuration = new OsuClientConfiguration
            {
                ClientId = 123,
                ClientSecret = "my-super-secret"
            })
            .ConfigureServices((ctx, services) => services.AddSingleton<IOsuService, OsuService>());
    }
}
```

> IOsuService.cs

```cs
internal interface IOsuService
{
    IAsyncEnumerable<IBeatmapset> GetLastRankedBeatmapsetsAsync(int count);
    Task<string> GetUserAvatarUrlAsync(string username);
}
```

> OsuService.cs

```cs
public class OsuService : IOsuService
{
    private readonly IOsuClient _client;

    public OsuService(IOsuClient client)
    {
        _client = client;
    }

    public async IAsyncEnumerable<IBeatmapset> GetLastRankedBeatmapsetsAsync(int count)
    {
        var builder = new BeatmapsetsLookupBuilder()
            .WithGameMode(GameMode.Osu)
            .WithConvertedBeatmaps()
            .WithCategory(BeatmapsetCategory.Ranked);

        await foreach (var beatmap in _client.EnumerateBeatmapsetsAsync(builder, BeatmapSorting.Ranked_Desc))
        {
            yield return beatmap;

            count--;
            if (count == 0)
            {
                break;
            }
        }
    }

    public async Task<string> GetUserAvatarUrlAsync(string username)
    {
        var user = await _client.GetUserAsync(username);
        return user.AvatarUrl.ToString();
    }
}
```

## Contributing

If you want to contribute, feel free to use Issues or Pull Requests!

### Todo: 
```
Beatmapset Discussions
 - /beatmapsets/discussions/posts (Get Beatmapset Discussion Posts)
 - /beatmapsets/discussions/votes (Get Beatmapset Discussion Votes)
 - /beatmapsets/discussions/ (Get Beatmapset Discussions)

Changelog
 - /changelog/{stream}/{build} (Get Changelog Build)
 - /changelog (Get Changelog Listing)
 - /changelog/{changelog} (Lookup Changelog Build)

Chat
 - /chat/new (Create New PM)

Comments
 - /comments (Get Comments)
 - /comments/{comment} (Get a Comment)

Forum
 - /forums/topics/{topic}/reply (Reply Topic)
 - /forums/topics (Create Topic)
 - /forums/topics/{topic} (Get Topic and Posts)
 - /forums/topics/{topic} (Edit Topic)
 - /forums/posts/{topic} (Edit Post)

Home
 - /search (Search | Users AND/OR Wiki pages) 

Multiplayer
 - /rooms/{room}/playlist/{playlist}/scores (Get Scores)

News
 - /news (Get News Listing)
 - /news/{news} (Get News Post)

Ranking
 - /spotlights (Get Spotlights)

Wiki
 - /wiki/{locale}/{path}

Undocumented
 - /beatmapsets/events
 - /matches
 - /matches/{match}
 - /rooms/{mode?}
 - /rooms/{room}/leaderboard
 - /rooms/{room}
 - /beatmapsets/lookup (doesn't seem to work?)
 - /friends
```

### Done already
```
Users
 - /me/{mode} (Get Own Data)
 - /users/{user}/kudosu (Get User Kudosu)
 - /users/{user}/scores/{types} (Get User Scores)
 - /users/{user}/beatmapsets/{type} (Get User Beatmaps)
 - /users/{user}/recent_activity (Get User Recent Activity)
 - /users/{user}/{mode?} (Get User)

Beatmaps
 - /beatmaps/{beatmap} (Get Beatmap)
 - /beatmaps/{beatmap}/scores/users/{user} (Get a User Beatmap score)
 - /beatmaps/{beatmap}/scores/users/{user}/all (Get all User Beatmap score)
 - /beatmaps/{beatmap}/scores (Get Beatmap scores)
 - /beatmaps/lookup (Lookup Beatmap)

Beatmapsets
 - /beatmapsets/{beatmapset} (Get Beatmapset)
 - /beatmapsets/search/{filters?} (Search beatmapsets)

Scores
 - /scores/{mode}/{score} (Get Score)
 - /scores/{mode}/{score}/download (Get Replay ; Needs Authorization Code Grant)

OAuth Tokens
 - /oauth/tokens/current (Revoke current token)

Misc
 - /seasonal-backgrounds (Get Current Seasonal Backgrounds ; No Auth)

Ranking
 - /rankings/{mode}/{type} (Get Ranking)
```

## Contact

You can join my personal Discord server: https://discord.gg/UugbeH8

## Thanks

Thanks to the following contributors: 
- [Naamloos](https://github.com/Naamloos)
- [Kamdzy](https://github.com/Kamdzy)
- [Bond-009](https://github.com/Bond-009)
- [jacksonrakena](https://github.com/jacksonrakena)
- [Piotrekol](https://github.com/Piotrekol)
- [Quahu](https://github.com/Quahu)
- [Kieran](https://github.com/k-boyle)
- [Francesco149](https://github.com/Francesco149) for Oppai.

Thanks to JetBrains for giving an open source license for their products!

<a href="https://www.jetbrains.com/?from=jensyl"><img src="imgs/jetbrains.svg" alt="JetBrains IDEs" width="150px"></img></a>
