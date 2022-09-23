using System;
using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class RankingSpotlightInformationJsonModel : JsonModel
{
      [JsonProperty("end_date")]
      public DateTime EndTime { get; set; }

	[JsonProperty("id")]
	public int Id { get; set; }

	[JsonProperty("mode_specific")]
	public bool ModeSpecific { get; set; }

	[JsonProperty("participant_count")]
	public int? ParticipantCount { get; set; }

	[JsonProperty("name")]
	public string Name { get; set; } = null!;

	[JsonProperty("start_date")]
	public DateTime StartDate { get; set; }

	[JsonProperty("type")]
	public string Type { get; set; } = null!;
}