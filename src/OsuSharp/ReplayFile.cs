using System;
using System.IO;
using System.Text;
using OsuSharp.Entities;
using OsuSharp.Enums;

namespace OsuSharp
{
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

    public sealed class ReplayFile
    {
        // These values appear in a .osr file in this 
        // Exact order.

        /// <summary>
        ///     Game mode of the replay (byte)
        /// </summary>
        public GameMode GameMode { get; private set; }

        /// <summary>
        ///     Version of the game when the replay was created
        /// </summary>
        public int OsuVersion { get; private set; }

        /// <summary>
        ///     osu! beatmap MD5 hash
        /// </summary>
        public string BeatmapHash { get; private set; }

        /// <summary>
        ///     Player name
        /// </summary>
        public string PlayerName { get; private set; }

        /// <summary>
        ///     osu! replay MD5 hash (includes certain properties of the replay)
        /// </summary>
        public string ReplayHash { get; private set; }

        /// <summary>
        ///     Number of 300s
        /// </summary>
        public short Amount300 { get; private set; }

        /// <summary>
        ///     Number of 100s in standard, 150s in Taiko, 100s in CTB, 200s in mania
        /// </summary>
        public short Amount100 { get; private set; }

        /// <summary>
        ///     Number of 50s in standard, small fruit in CTB, 50s in mania
        /// </summary>
        public short Amount50 { get; private set; }

        /// <summary>
        ///     Number of Gekis in standard, Max 300s in mania
        /// </summary>
        public short AmountGeki { get; private set; }

        /// <summary>
        ///     Number of Katus in standard, 100s in mania
        /// </summary>
        public short AmountKatu { get; private set; }

        /// <summary>
        ///     Number of misses
        /// </summary>
        public short AmountMiss { get; private set; }

        /// <summary>
        ///     Total score displayed on the score report
        /// </summary>
        public int TotalScore { get; private set; }

        /// <summary>
        ///     Greatest combo displayed on the score report
        /// </summary>
        public short MaxCombo { get; private set; }

        /// <summary>
        ///     Perfect/full combo (1 = no misses and no slider breaks and no early finished sliders)
        /// </summary>
        public bool Perfect { get; private set; }

        /// <summary>
        ///     Mods used
        /// </summary>
        public Mode Mods { get; private set; }

        /// <summary>
        ///     Life bar graph: comma separated pairs u/v, where u is the time in milliseconds into the song and v is a floating point value from 0 - 1 that represents the amount of life you have at the given time (0 = life bar is empty, 1= life bar is full)
        /// </summary>
        public string LifebarGraph { get; private set; }

        /// <summary>
        ///     Time stamp (Windows ticks)
        /// </summary>
        public long Timestamp { get; private set; }

        /// <summary>
        ///     Replay data length
        /// </summary>
        public int ReplayLength { get; private set; }

        /// <summary>
        ///     Compressed replay data
        /// </summary>
        public byte[] ReplayData { get; private set; }

        /// <summary>
        ///     That's literally in osu's docs. unknown. welp.
        /// </summary>
        public long Unknown { get; private set; }

        /// <summary>
        ///     Loads a Replay file from a stream
        /// </summary>
        /// <param name="file">file to load</param>
        /// <returns></returns>
        public static ReplayFile FromStream(Stream file)
        {
            file.Position = 0;
            var binreader = new BinaryReader(file);
            var reader = new ReplayReader(binreader);
            var replay = new ReplayFile
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

            replay.ReplayData = reader.ReadNextBytes(replay.ReplayLength);
            replay.Unknown = reader.ReadNextLong();
            reader.Readlastdata();

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
            writer.WriteNextBytes(ReplayData);
            writer.WriteNextLong(Unknown);
        }

        /// <summary>
        ///     Create a replay from api entities.
        ///     You shouldn't mix up replays, users, beatmaps and scores, but.. you could.
        /// </summary>
        /// <param name="replay">Replay entity</param>
        /// <param name="user">User entity</param>
        /// <param name="score">Score entity</param>
        /// <param name="beatmap">Beatmap entity</param>
        /// <returns></returns>
        public static ReplayFile CreateReplayFile(Replay replay, User user, Score score, Beatmap beatmap)
        {
            var playbytes = Convert.FromBase64String(replay.Content);
            return new ReplayFile
            {
                Amount300 = (short)score.Count300,
                Amount100 = (short)score.Count100,
                Amount50 = (short)score.Count50,
                AmountGeki = (short)score.Geki,
                AmountKatu = (short)score.Katu,
                AmountMiss = (short)score.Miss,
                BeatmapHash = beatmap.FileMd5,
                GameMode = beatmap.GameMode,
                TotalScore = (int)score.TotalScore,
                ReplayLength = playbytes.Length,
                ReplayData = playbytes,
                ReplayHash = "idk where to get replay hash",
                MaxCombo = (short)score.MaxCombo,
                LifebarGraph = "",
                Mods = score.Mods,
                Perfect = score.Perfect,
                Timestamp = score.Date.Ticks,
                OsuVersion = 0,
                PlayerName = score.Username,
                Unknown = score.Userid // whatever it's both a long
            };
        }

        internal ReplayFile()
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
}
