using System.Collections.Generic;
using System.Text;
using OsuSharp.Interfaces;

namespace OsuSharp.Domain
{
    public static class ReplayExtensions
    {
        public static IReadOnlyList<ReplayMoveData> GetReplayMoveData(this IReplay replay)
        {
            var decompressedData = SevenZipLZMAHelper.Decompress(replay.CompressedReplayData.ToArray());
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
}
