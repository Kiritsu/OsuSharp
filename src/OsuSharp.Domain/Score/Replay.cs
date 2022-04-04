using System;
using System.IO;
using System.Text;
using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public class Replay : IReplay
{
    /// <summary>
    /// Gets the game mode of the replay.
    /// </summary>
    public GameMode GameMode { get; private init; }

    /// <summary>
    /// Gets the version of the game when the replay was created (ex. 20131216).
    /// </summary>
    public int OsuVersion { get; private init; }

    /// <summary>
    /// Gets the osu! beatmap MD5 hash.
    /// </summary>
    public string? BeatmapHash { get; private init; }

    /// <summary>
    /// Gets the player name.
    /// </summary>
    public string? PlayerName { get; private init; }

    /// <summary>
    /// Gets the osu! replay MD5 hash (includes certain properties of the replay).
    /// </summary>
    public string? ReplayHash { get; private init; }

    /// <summary>
    /// Gets the number of 300s.
    /// </summary>
    public short Amount300 { get; private init; }

    /// <summary>
    /// Gets the number of 100s in standard, 150s in Taiko, 100s in CTB, 100s in mania.
    /// </summary>
    public short Amount100 { get; private init; }

    /// <summary>
    /// Gets the number of 50s in standard, small fruit in CTB, 50s in mania.
    /// </summary>
    public short Amount50 { get; private init; }

    /// <summary>
    /// Gets the number of Gekis in standard, Max 300s in mania.
    /// </summary>
    public short AmountGeki { get; private init; }

    /// <summary>
    /// Gets the number of Katus in standard, 200s in mania.
    /// </summary>
    public short AmountKatu { get; private init; }

    /// <summary>
    /// Gets the number of misses.
    /// </summary>
    public short AmountMiss { get; private init; }

    /// <summary>
    /// Gets the total score displayed on the score report.
    /// </summary>
    public int TotalScore { get; private init; }

    /// <summary>
    /// Gets the greatest combo displayed on the score report.
    /// </summary>
    public short MaxCombo { get; private init; }

    /// <summary>
    /// Gets whether the score is a perfect or full combo.
    /// </summary>
    public bool Perfect { get; private init; }

    /// <summary>
    /// Gets the mods used.
    /// </summary>
    public Mods Mods { get; private init; }

    /// <summary>
    /// Gets the life bar graph: comma separated pairs u/v, where u is the time in milliseconds into the song and v is a floating point value from 0 - 1 that represents the amount of life you have at the given time (0 = life bar is empty, 1 = life bar is full).
    /// </summary>
    public string? LifebarGraph { get; private init; }

    /// <summary>
    /// Gets the time stamp (Windows ticks).
    /// </summary>
    public DateTime Timestamp { get; private init; }

    /// <summary>
    /// Gets the length in bytes of compressed replay data.
    /// </summary>
    public int ReplayLength { get; private init; }

    /// <summary>
    /// Gets the compressed replay data.
    /// </summary>
    public ReadOnlyMemory<byte> CompressedReplayData { get; private set; }

    /// <summary>
    /// Gets the score id.
    /// </summary>
    public long ScoreId { get; private set; }

    /// <summary>
    /// Gets the additional mod information. Only present if Target Practice is enabled.
    /// </summary>
    public double AdditionalMods { get; private set; }

    public static Replay FromStream(Stream stream)
    {
        stream.Position = 0;
        var binreader = new BinaryReader(stream);
        var reader = new ReplayReader(binreader);
        var replay = new Replay
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
            Mods = (Mods)reader.ReadNextInt(),
            LifebarGraph = reader.ReadNextOsuString(),
            Timestamp = new DateTime(reader.ReadNextLong()),
            ReplayLength = reader.ReadNextInt()
        };

        replay.CompressedReplayData = reader.ReadNextBytes(replay.ReplayLength).AsMemory();
        replay.ScoreId = reader.ReadNextLong();

        try
        {
            replay.AdditionalMods = reader.ReadNextDouble();
        }
        catch
        {
            // ignored
        }

        return replay;
    }

    public void CopyTo(Stream stream)
    {
        stream.Position = 0;
        var binwriter = new BinaryWriter(stream);
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
        writer.WriteNextLong(Timestamp.Ticks);
        writer.WriteNextInt(ReplayLength);
        writer.WriteNextBytes(CompressedReplayData.ToArray());
        writer.WriteNextLong(ScoreId);
        writer.WriteNextDouble(AdditionalMods);
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

    public string? ReadNextOsuString()
    {
        // 0x00 if not present
        // 0x0b if present
        
        var isPresent = ReadNextByte();
        if (isPresent != 0x0b)
        {
            return null;
        }
        
        var length = (int)ReadNextULeb128();
        var text = Reader.ReadBytes(length);
        
        return Encoding.UTF8.GetString(text);
    }

    public short ReadNextShort()
    {
        return Reader.ReadInt16();
    }

    public bool ReadNextBool()
    {
        return Reader.ReadBoolean();
    }

    public long ReadNextLong()
    {
        return Reader.ReadInt64();
    }

    public byte[] ReadNextBytes(int length)
    {
        return Reader.ReadBytes(length);
    }

    public double ReadNextDouble()
    {
        return Reader.ReadDouble();
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

    public byte[] ReadLastData()
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

    public void WriteNextOsuString(string? value)
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

    public void WriteNextDouble(double value)
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