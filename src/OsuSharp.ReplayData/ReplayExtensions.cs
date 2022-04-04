using System;
using System.Collections.Generic;
using System.Text;
using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

/// <summary>
/// Represents the static class with the extension methods to the <see cref="IReplay"/> class.
/// </summary>
public static class ReplayExtensions
{
    /// <summary>
    /// Decompress and retrieve the <see cref="ReplayMoveData"/> from the current replay.
    /// </summary>
    /// <param name="replay">Instance of the replay.</param>
    public static IReadOnlyList<ReplayMoveData> GetReplayMoveData(this IReplay replay)
    {
        if (replay == null)
        {
            throw new ArgumentNullException(nameof(replay));
        }

        var decompressedData = SevenZipLzmaHelper.Decompress(replay.CompressedReplayData.ToArray());
        var decompressedDataString = Encoding.UTF8.GetString(decompressedData);

        var moveData = new List<ReplayMoveData>();
        foreach (var replayData in decompressedDataString.Split(','))
        {
            var contents = replayData.Split('|');
            if (contents.Length < 4)
            {
                continue;
            }

            moveData.Add(new ReplayMoveData(long.Parse(contents[0]), float.Parse(contents[1]), float.Parse(contents[2]), int.Parse(contents[3])));
        }

        return moveData.AsReadOnly();
    }
}