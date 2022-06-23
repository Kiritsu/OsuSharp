using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using OsuSharp.Legacy.Entities;
using OsuSharp.Legacy.Enums;

namespace OsuSharp.Legacy;

public struct MoveData
{
    public TimeSpan ElapsedTimeSincePreviousAction { get; }
    public float CursorX { get; }
    public float CursorY { get; }
    public OsuKey Keys { get; }

    public MoveData(long w, float x, float y, int z)
    {
        ElapsedTimeSincePreviousAction = TimeSpan.FromMilliseconds(w);
        CursorX = x;
        CursorY = y;
        Keys = (OsuKey)z;
    }
}

[Flags]
public enum OsuKey
{
    M1 = 1,
    M2 = 2,
    K1 = 4,
    K2 = 8,
    Smoke = 16
}

/*
 * Data types from osu's documentation:
 * 
 * Byte (1 byte): A single 8 bit value
 * Short (2 bytes): A 2-byte little endian value
 * Integer (4 bytes): A 4-byte little endian value
 * Long (8 bytes): A 8-byte little endian value
 * ULEB128 (variable): A variable length integer
 */

/*
 * String:
 * Has three parts; a single byte which will be either 0x00,
 * indicating that the next two parts are not present,
 * or 0x0b (decimal 11),
 * indicating that the next two parts are present.
 * If it is 0x0b, there will then be a ULEB128,
 * representing the byte length of the following string,
 * and then the string itself, encoded in UTF-8.
 */

public sealed class LegacyReplayFile
{
    // These values appear in a .osr file in this 
    // Exact order.

    /// <summary>
    ///     Game mode of the replay (byte)
    /// </summary>
    public GameMode GameMode { get; private init; }

    /// <summary>
    ///     Version of the game when the replay was created
    /// </summary>
    public int OsuVersion { get; private init; }

    /// <summary>
    ///     osu! beatmap MD5 hash
    /// </summary>
    public string BeatmapHash { get; private init; }

    /// <summary>
    ///     Player name
    /// </summary>
    public string PlayerName { get; private init; }

    /// <summary>
    ///     osu! replay MD5 hash (includes certain properties of the replay)
    /// </summary>
    public string ReplayHash { get; private set; }

    /// <summary>
    ///     Number of 300s
    /// </summary>
    public short Amount300 { get; private init; }

    /// <summary>
    ///     Number of 100s in standard, 150s in Taiko, 100s in CTB, 200s in mania
    /// </summary>
    public short Amount100 { get; private init; }

    /// <summary>
    ///     Number of 50s in standard, small fruit in CTB, 50s in mania
    /// </summary>
    public short Amount50 { get; private init; }

    /// <summary>
    ///     Number of Gekis in standard, Max 300s in mania
    /// </summary>
    public short AmountGeki { get; private init; }

    /// <summary>
    ///     Number of Katus in standard, 100s in mania
    /// </summary>
    public short AmountKatu { get; private init; }

    /// <summary>
    ///     Number of misses
    /// </summary>
    public short AmountMiss { get; private init; }

    /// <summary>
    ///     Total score displayed on the score report
    /// </summary>
    public int TotalScore { get; private init; }

    /// <summary>
    ///     Greatest combo displayed on the score report
    /// </summary>
    public short MaxCombo { get; private init; }

    /// <summary>
    ///     Perfect/full combo (1 = no misses and no slider breaks and no early finished sliders)
    /// </summary>
    public bool Perfect { get; private init; }

    /// <summary>
    ///     Mods used
    /// </summary>
    public Mode Mods { get; private init; }

    /// <summary>
    ///     Life bar graph: comma separated pairs u/v, where u is the time in milliseconds into the song and v is a floating point value from 0 - 1 that represents the amount of life you have at the given time (0 = life bar is empty, 1= life bar is full)
    /// </summary>
    public string LifebarGraph { get; private init; }

    /// <summary>
    ///     Time stamp (Windows ticks)
    /// </summary>
    public long Timestamp { get; private init; }

    /// <summary>
    ///     Replay data length
    /// </summary>
    public int ReplayLength { get; private init; }

    /// <summary>
    ///     Compressed replay data
    /// </summary>
    public byte[] CompressedReplayData { get; private set; }

    /// <summary>
    ///     Decompressed replay data
    /// </summary>
    public string DecompressedReplayData { get; private set; }

    /// <summary>
    ///     Replay data as move data.
    /// </summary>
    public IReadOnlyList<MoveData> ReplayData { get; private set; }

    /// <summary>
    ///     That's literally in osu's docs. unknown. welp.
    /// </summary>
    public long OnlineScoreId { get; private set; }

    /// <summary>
    ///     Loads a Replay file from a stream
    /// </summary>
    /// <param name="file">file to load</param>
    /// <returns></returns>
    public static LegacyReplayFile FromStream(Stream file)
    {
        file.Position = 0;
        var binreader = new BinaryReader(file);
        var reader = new ReplayReader(binreader);
        var replay = new LegacyReplayFile
        {
            GameMode = (GameMode)reader.ReadNextByte(),
            OsuVersion = reader.ReadNextInt(),
            BeatmapHash = reader.ReadNextOsuString(),
            PlayerName = reader.ReadNextOsuString(),
            ReplayHash = reader.ReadNextOsuString(),
            Amount300 = reader.ReadNextShort(),
            Amount100 = reader.ReadNextShort(),
            Amount50 = reader.ReadNextShort(),
            AmountGeki = reader.ReadNextShort(),
            AmountKatu = reader.ReadNextShort(),
            AmountMiss = reader.ReadNextShort(),
            TotalScore = reader.ReadNextInt(),
            MaxCombo = reader.ReadNextShort(),
            Perfect = reader.ReadNextBool(),
            Mods = (Mode)reader.ReadNextInt(),
            LifebarGraph = reader.ReadNextOsuString(),
            Timestamp = reader.ReadNextLong(),
            ReplayLength = reader.ReadNextInt()
        };

        replay.CompressedReplayData = reader.ReadNextBytes(replay.ReplayLength);
        var decompressedData = SevenZipLZMAHelper.Decompress(replay.CompressedReplayData);
        replay.DecompressedReplayData = Encoding.UTF8.GetString(decompressedData);

        var moveData = new List<MoveData>();
        foreach (var replayData in replay.DecompressedReplayData.Split(','))
        {
            var contents = replayData.Split('|');
            if (contents.Length < 4)
            {
                continue;
            }

            moveData.Add(new MoveData(long.Parse(contents[0]), float.Parse(contents[1]), float.Parse(contents[2]), int.Parse(contents[3])));
        }
        replay.ReplayData = moveData.AsReadOnly();

        replay.OnlineScoreId = reader.ReadNextLong();

        return replay;
    }

    /// <summary>
    ///     Writes a replay file to a stream
    /// </summary>
    /// <param name="file">stream to write to</param>
    public void ToStream(Stream file)
    {
        file.Position = 0;
        var binwriter = new BinaryWriter(file);
        var writer = new ReplayWriter(binwriter);

        writer.WriteNextByte((byte)GameMode);
        writer.WriteNextInt(OsuVersion);
        writer.WriteNextOsuString(BeatmapHash);
        writer.WriteNextOsuString(PlayerName);
        writer.WriteNextOsuString(ReplayHash);
        writer.WriteNextShort(Amount300);
        writer.WriteNextShort(Amount100);
        writer.WriteNextShort(Amount50);
        writer.WriteNextShort(AmountGeki);
        writer.WriteNextShort(AmountKatu);
        writer.WriteNextShort(AmountMiss);
        writer.WriteNextInt(TotalScore);
        writer.WriteNextShort(MaxCombo);
        writer.WriteNextBool(Perfect);
        writer.WriteNextInt((int)Mods);
        writer.WriteNextOsuString(LifebarGraph);
        writer.WriteNextLong(Timestamp);
        writer.WriteNextInt(ReplayLength);
        writer.WriteNextBytes(CompressedReplayData);
        writer.WriteNextLong(OnlineScoreId);
    }

    /// <summary>
    ///     Create a replay from api entities.
    ///     You shouldn't mix up replays, users, beatmaps and scores, but.. you could.
    /// </summary>
    /// <param name="entityLegacyReplay">Replay entity</param>
    /// <param name="legacyScore">Score entity</param>
    /// <param name="legacyBeatmap">Beatmap entity</param>
    /// <returns></returns>
    public static LegacyReplayFile CreateReplayFile(LegacyReplay entityLegacyReplay, LegacyScore legacyScore, LegacyBeatmap legacyBeatmap)
    {
        var playbytes = Convert.FromBase64String(entityLegacyReplay.Content);
        var replay = new LegacyReplayFile
        {
            Amount300 = (short)legacyScore.Count300,
            Amount100 = (short)legacyScore.Count100,
            Amount50 = (short)legacyScore.Count50,
            AmountGeki = (short)legacyScore.Geki,
            AmountKatu = (short)legacyScore.Katu,
            AmountMiss = (short)legacyScore.Miss,
            BeatmapHash = legacyBeatmap.FileMd5,
            GameMode = legacyBeatmap.GameMode,
            TotalScore = (int)legacyScore.TotalScore,
            ReplayLength = playbytes.Length,
            CompressedReplayData = playbytes,
            MaxCombo = (short)legacyScore.MaxCombo,
            LifebarGraph = "",
            Mods = legacyScore.Mods,
            Perfect = legacyScore.Perfect,
            Timestamp = legacyScore.Date.Value.Ticks,
            OsuVersion = 0,
            PlayerName = legacyScore.Username,
            OnlineScoreId = legacyScore.ScoreId ?? 0 // whatever it's both a long
        };

        var hash = MD5.HashData(Encoding.UTF8.GetBytes($"{replay.MaxCombo}osu{replay.PlayerName}{legacyBeatmap.FileMd5}{replay.TotalScore}{legacyScore.Rank}"));
        replay.ReplayHash = string.Join("", hash.Select(x => x.ToString("X2")));
        
        var decompressedData = SevenZipLZMAHelper.Decompress(replay.CompressedReplayData);
        replay.DecompressedReplayData = Encoding.UTF8.GetString(decompressedData);

        var moveData = new List<MoveData>();
        foreach (var replayData in replay.DecompressedReplayData.Split(','))
        {
            var contents = replayData.Split('|');
            if (contents.Length < 4)
            {
                continue;
            }

            moveData.Add(new MoveData(long.Parse(contents[0]), float.Parse(contents[1]), float.Parse(contents[2]), int.Parse(contents[3])));
        }
        replay.ReplayData = moveData.AsReadOnly();

        return replay;
    }

    internal LegacyReplayFile()
    {

    }
}

internal class ReplayReader
{
    private BinaryReader Reader { get; }

    public ReplayReader(BinaryReader reader)
    {
        Reader = reader;
    }

    public byte ReadNextByte()
    {
        return Reader.ReadByte();
    }

    public int ReadNextInt()
    {
        return Reader.ReadInt32();
    }

    public string ReadNextOsuString()
    {
        var ispresent = ReadNextByte();
        // 0x00 if not present and 0x0b if present
        if (ispresent == 0x0b)
        {
            var length = (int)ReadNextULeb128();
            var text = Reader.ReadBytes(length);
            return Encoding.UTF8.GetString(text);
        }

        return null;
    }

    public short ReadNextShort()
    {
        return Reader.ReadInt16();
    }

    public bool ReadNextBool()
    {
        return Reader.ReadBoolean();
    }

    // for reading that unknown :^)
    public long ReadNextLong()
    {
        return Reader.ReadInt64();
    }

    public byte[] ReadNextBytes(int length)
    {
        return Reader.ReadBytes(length);
    }

    private uint ReadNextULeb128()
    {
        uint result = 0;
        var shift = 0;
        while (true)
        {
            var b = Reader.ReadByte();
            result |= (uint)((b & 0x7F) << shift);
            if ((b & 0x80) == 0)
            {
                break;
            }

            shift += 7;
        }
        return result;
    }

    public byte[] Readlastdata()
    {
        return Reader.ReadBytes((int)(Reader.BaseStream.Length - Reader.BaseStream.Position));
    }
}

internal class ReplayWriter
{
    private BinaryWriter Writer { get; }

    public ReplayWriter(BinaryWriter writer)
    {
        Writer = writer;
    }

    public void WriteNextByte(byte value)
    {
        Writer.Write(value);
    }

    public void WriteNextInt(int value)
    {
        Writer.Write(value);
    }

    public void WriteNextOsuString(string value)
    {
        if (value == null)
        {
            WriteNextByte(0x00);
            return;
        }
        var bytes = Encoding.UTF8.GetBytes(value);
        WriteNextByte(0x0b);
        WriteNextULeb128((uint)bytes.Length);
        Writer.Write(bytes);
    }

    public void WriteNextShort(short value)
    {
        Writer.Write(value);
    }

    public void WriteNextBool(bool value)
    {
        Writer.Write(value);
    }

    // for reading that unknown :^)
    public void WriteNextLong(long value)
    {
        Writer.Write(value);
    }

    public void WriteNextBytes(byte[] value)
    {
        Writer.Write(value);
    }

    private void WriteNextULeb128(uint value)
    {
        do
        {
            var b = (byte)(value & 0x7F);
            value >>= 7;
            if (value != 0) /* more bytes to come */
            {
                b |= 0x80;
            }

            Writer.Write(b);
        } while (value != 0);
    }
}