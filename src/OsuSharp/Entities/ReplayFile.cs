using OsuSharp.Endpoints;
using OsuSharp.Misc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OsuSharp.Entities
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

    public class ReplayFile
    {
        // These values appear in a .osr file in this 
        // Exact order.

        /// <summary>
        /// Game mode of the replay (byte)
        /// </summary>
        public GameMode GameMode;
        /// <summary>
        /// Version of the game when the replay was created
        /// </summary>
        public int OsuVersion;
        /// <summary>
        /// osu! beatmap MD5 hash
        /// </summary>
        public string BeatmapHash;
        /// <summary>
        /// Player name
        /// </summary>
        public string PlayerName;
        /// <summary>
        /// osu! replay MD5 hash (includes certain properties of the replay)
        /// </summary>
        public string ReplayHash;
        /// <summary>
        /// Number of 300s
        /// </summary>
        public short Amount300;
        /// <summary>
        /// Number of 100s in standard, 150s in Taiko, 100s in CTB, 200s in mania
        /// </summary>
        public short Amount100;
        /// <summary>
        /// Number of 50s in standard, small fruit in CTB, 50s in mania
        /// </summary>
        public short Amount50;
        /// <summary>
        /// Number of Gekis in standard, Max 300s in mania
        /// </summary>
        public short AmountGeki;
        /// <summary>
        /// Number of Katus in standard, 100s in mania
        /// </summary>
        public short AmountKatu;
        /// <summary>
        /// Number of misses
        /// </summary>
        public short AmountMiss;
        /// <summary>
        /// Total score displayed on the score report
        /// </summary>
        public int TotalScore;
        /// <summary>
        /// Greatest combo displayed on the score report
        /// </summary>
        public short MaxCombo;
        /// <summary>
        /// Perfect/full combo (1 = no misses and no slider breaks and no early finished sliders)
        /// </summary>
        public bool Perfect;
        /// <summary>
        /// Mods used
        /// </summary>
        public Mods Mods;
        /// <summary>
        /// Life bar graph: comma separated pairs u/v, where u is the time in milliseconds into the song and v is a floating point value from 0 - 1 that represents the amount of life you have at the given time (0 = life bar is empty, 1= life bar is full)
        /// </summary>
        public string LifebarGraph;
        /// <summary>
        /// Time stamp (Windows ticks)
        /// </summary>
        public long Timestamp;
        /// <summary>
        /// Replay data length
        /// </summary>
        public int ReplayLength;
        /// <summary>
        /// Compressed replay data
        /// </summary>
        public byte[] ReplayData;
        /// <summary>
        /// That's literally in osu's docs. unknown. welp.
        /// </summary>
        public long Unknown;

        /// <summary>
        /// Loads a Replay file from a stream
        /// </summary>
        /// <param name="file">file to load</param>
        /// <returns></returns>
        public static ReplayFile FromStream(Stream file)
        {
            file.Position = 0;
            var binreader = new BinaryReader(file);
            var reader = new ReplayReader(binreader);
            var replay = new ReplayFile();

            replay.GameMode = (GameMode)reader.ReadNextByte();
            replay.OsuVersion = reader.ReadNextInt();
            replay.BeatmapHash = reader.ReadNextOsuString();
            replay.PlayerName = reader.ReadNextOsuString();
            replay.ReplayHash = reader.ReadNextOsuString();
            replay.Amount300 = reader.ReadNextShort();
            replay.Amount100 = reader.ReadNextShort();
            replay.Amount50 = reader.ReadNextShort();
            replay.AmountGeki = reader.ReadNextShort();
            replay.AmountKatu = reader.ReadNextShort();
            replay.AmountMiss = reader.ReadNextShort();
            replay.TotalScore = reader.ReadNextInt();
            replay.MaxCombo = reader.ReadNextShort();
            replay.Perfect = reader.ReadNextBool();
            replay.Mods = (Mods)reader.ReadNextInt();
            replay.LifebarGraph = reader.ReadNextOsuString();
            replay.Timestamp = reader.ReadNextLong();
            replay.ReplayLength = reader.ReadNextInt();
            replay.ReplayData = reader.ReadNextBytes(replay.ReplayLength);
            replay.Unknown = reader.ReadNextLong();
            var last = reader.readlastdata();

            return replay;
        }

        /// <summary>
        /// Writes a replay file to a stream
        /// </summary>
        /// <param name="file">stream to write to</param>
        public void ToStream(Stream file)
        {
            file.Position = 0;
            var binwriter = new BinaryWriter(file);
            var writer = new ReplayWriter(binwriter);

            writer.WriteNextByte((byte)this.GameMode);
            writer.WriteNextInt(this.OsuVersion);
            writer.WriteNextOsuString(this.BeatmapHash);
            writer.WriteNextOsuString(this.PlayerName);
            writer.WriteNextOsuString(this.ReplayHash);
            writer.WriteNextShort(this.Amount300);
            writer.WriteNextShort(this.Amount100);
            writer.WriteNextShort(this.Amount50);
            writer.WriteNextShort(this.AmountGeki);
            writer.WriteNextShort(this.AmountKatu);
            writer.WriteNextShort(this.AmountMiss);
            writer.WriteNextInt(TotalScore);
            writer.WriteNextShort(this.MaxCombo);
            writer.WriteNextBool(this.Perfect);
            writer.WriteNextInt((int)this.Mods);
            writer.WriteNextOsuString(this.LifebarGraph);
            writer.WriteNextLong(this.Timestamp);
            writer.WriteNextInt(ReplayLength);
            writer.WriteNextBytes(this.ReplayData);
            writer.WriteNextLong(this.Unknown);
        }

        /// <summary>
        /// Create a replay from api entities.
        /// You shouldn't mix up replays, users, beatmaps and scores, but.. you could.
        /// </summary>
        /// <param name="replay">Replay entity</param>
        /// <param name="user">User entity</param>
        /// <param name="score">Score entity</param>
        /// <param name="beatmap">Beatmap entity</param>
        /// <returns></returns>
        public ReplayFile CreateReplayFile(Replay replay, User user, Score score, Beatmap beatmap)
        {
            var playbytes = Convert.FromBase64String(replay.Content);
            return new ReplayFile()
            {
                Amount300 = (short)score.Count300,
                Amount100 = (short)score.Count100,
                Amount50 = (short)score.Count50,
                AmountGeki = (short)score.Geki,
                AmountKatu = (short)score.Katu,
                AmountMiss = (short)score.Miss,
                BeatmapHash = beatmap.FileMd5,
                GameMode = beatmap.GameMode,
                TotalScore = (int)score.ScorePoints,
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
    }

    internal class ReplayReader
    {
        BinaryReader _reader;
        public ReplayReader(BinaryReader reader)
        {
            this._reader = reader;
        }

        public byte ReadNextByte()
        {
            return _reader.ReadByte();
        }

        public int ReadNextInt()
        {
            return _reader.ReadInt32();
        }

        public string ReadNextOsuString()
        {
            byte ispresent = ReadNextByte();
            // 0x00 if not present and 0x0b if present
            if (ispresent == 0x0b)
            {
                int length = (int)ReadNextULeb128();
                byte[] text = _reader.ReadBytes(length);
                return Encoding.UTF8.GetString(text);
            }
            return null;
        }

        public short ReadNextShort()
        {
            return _reader.ReadInt16();
        }

        public bool ReadNextBool()
        {
            return _reader.ReadBoolean();
        }

        public long ReadNextLong()
        {
            // for reading that unknown :^)
            return _reader.ReadInt64();
        }

        public byte[] ReadNextBytes(int length)
        {
            return _reader.ReadBytes(length);
        }

        private uint ReadNextULeb128()
        {
            uint result = 0;
            int shift = 0;
            while (true)
            {
                byte b = _reader.ReadByte();
                result |= (uint)((b & 0x7F) << shift);
                if ((b & 0x80) == 0)
                    break;
                shift += 7;
            }
            return result;
        }
        public byte[] readlastdata()
        {
            return _reader.ReadBytes((int)(_reader.BaseStream.Length - _reader.BaseStream.Position));
        }
    }

    internal class ReplayWriter
    {
        BinaryWriter _writer;
        public ReplayWriter(BinaryWriter writer)
        {
            this._writer = writer;
        }

        public void WriteNextByte(byte value)
        {
            _writer.Write(value);
        }

        public void WriteNextInt(int value)
        {
            _writer.Write(value);
        }

        public void WriteNextOsuString(string value)
        {
            if(value == null)
            {
                WriteNextByte(0x00);
                return;
            }
            var bytes = Encoding.UTF8.GetBytes(value);
            WriteNextByte(0x0b);
            WriteNextULeb128((uint)bytes.Length);
            _writer.Write(bytes);
        }

        public void WriteNextShort(short value)
        {
            _writer.Write(value);
        }

        public void WriteNextBool(bool value)
        {
            _writer.Write(value);
        }

        public void WriteNextLong(long value)
        {
            // for reading that unknown :^)
            Console.WriteLine("Obviously writing a long");
            _writer.Write(value);
        }

        public void WriteNextBytes(byte[] value)
        {
            _writer.Write(value);
        }

        private void WriteNextULeb128(uint Value)
        {
            do
            {
                byte b = (byte)(Value & 0x7F);
                Value >>= 7;
                if (Value != 0) /* more bytes to come */
                    b |= 0x80;
                _writer.Write(b);
            } while (Value != 0);
        }
    }
}
