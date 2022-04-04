using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OsuSharp.Domain;

namespace OsuSharp.Net.Serialization;

internal class EventConverter : JsonConverter<Event>
{
    public static readonly EventConverter Instance = new();

    public override void WriteJson(JsonWriter writer, Event? value, JsonSerializer serializer)
    {
        serializer.Serialize(writer, value);
    }

    public override Event ReadJson(JsonReader reader, Type objectType, Event? existingValue, bool hasExistingValue,
        JsonSerializer serializer)
    {
        var jo = JObject.Load(reader);
        if (!jo.ContainsKey("type"))
        {
            throw new InvalidOperationException("Unknown event.");
        }

        var typeString = jo["type"]!.Value<string>();
        if (!Enum.TryParse<EventType>(typeString, true, out var type))
        {
            throw new InvalidOperationException("Unknown event.");
        }

        return type switch
        {
            EventType.Achievement => jo.ToObject<AchievementEvent>()!,
            EventType.BeatmapPlaycount => jo.ToObject<BeatmapPlaycountEvent>()!,
            EventType.BeatmapsetApprove => jo.ToObject<BeatmapsetApproveEvent>()!,
            EventType.BeatmapsetDelete => jo.ToObject<BeatmapsetDeleteEvent>()!,
            EventType.BeatmapsetRevive => jo.ToObject<BeatmapsetReviveEvent>()!,
            EventType.BeatmapsetUpdate => jo.ToObject<BeatmapsetUpdateEvent>()!,
            EventType.BeatmapsetUpload => jo.ToObject<BeatmapsetUploadEvent>()!,
            EventType.Rank => jo.ToObject<RankEvent>()!,
            EventType.RankLost => jo.ToObject<RankLostEvent>()!,
            EventType.UserSupportAgain => jo.ToObject<UserSupportAgainEvent>()!,
            EventType.UserSupportFirst => jo.ToObject<UserSupportFirstEvent>()!,
            EventType.UserSupportGift => jo.ToObject<UserSupportGiftEvent>()!,
            EventType.UsernameChange => jo.ToObject<UsernameChangeEvent>()!,
            EventType.Unknown => throw new NotSupportedException("This event is not supported."),
            _ => throw new NotSupportedException("This event is not supported.")
        };
    }
}