﻿using System;
using System.Text;
using CustomTools.Extensions.Core;
using CustomTools.Extensions.Core.Array;
using Tools.HexBinDec;
using Tools.Time;


namespace Buffers
{
    public class ByteBuffer : IDisposable
    {
        private const int BYTE_SIZE = 1;
        private const int SHORT_SIZE = 2;
        private const int INT_SIZE = 4;
        private const int LONG_SIZE = 8;
        private const int DOUBLE_SIZE = 8;

        public const bool LITTLE_ENDING = true;
        public const bool BIG_ENDING = false;

        private const bool DEFAULT_ENDING = BIG_ENDING;

        private bool littleEndian = DEFAULT_ENDING;
        private Bytes buffer = new Bytes();


        // Zigzag encodes a signed 32bit integer so that it can be effectively used with varint encoding.
        public static uint ZigZagEncode32(int n)
        {
            return (uint)((n << 1) ^ (n >> 31)); // ref: src/google/protobuf/wire_format_lite.h
        }

        // Decodes a zigzag encoded signed 32bit integer.
        public static int ZigZagDecode32(uint n)
        {
            return (int)(n >> 1) ^ -((int)(n & 1)); // ref: src/google/protobuf/wire_format_lite.h
        }

        // Calculates the actual number of bytes required to store a 32bit base 128 variable-length integer.
        private static int CalculateVarint32(int n)
        {
            // ref: src/google/protobuf/io/coded_stream.cc
            var value = (uint)n;
            if (value < 1 << 7) return 1;
            if (value < 1 << 14) return 2;
            if (value < 1 << 21) return 3;
            if (value < 1 << 28) return 4;
            return 5;
        }

        public ByteBuffer(bool littleEndian)
        {
            this.littleEndian = littleEndian;
        }

        public ByteBuffer(bool littleEndian, byte[] raw) : this(littleEndian)
        {
            buffer.AddRange(raw);
        }

        public void Dispose() => buffer.Clear();

        public byte[] ToArray() => buffer.ToArray();

        public override string ToString() => buffer.IsNullOrEmpty() ? string.Empty : ToArray().ToDecimalString(',');

        public void Print()
        {
            CustomTools.Console.Log(new StringBuilder().Append('[').Append(ToString()).Append(']').ToString());
        }

        public string ToHex() => buffer.IsNullOrEmpty() ? string.Empty : ToArray().ToHexString();

        public string ToBinary() => buffer.IsNullOrEmpty() ? string.Empty : ToArray().ToBinaryString();

        #region bool
        // Writes a boolean.
        public ByteBuffer WriteBool(bool value)
        {
            // false >= value <= true
            return WriteUInt8((byte)(value ? 1 : 0));
        }

        // Reads a boolean.
        public bool ReadBool() => ReadUInt8() > 0;
        #endregion

        #region byte
        // Writes an 8bit unsigned integer.
        public ByteBuffer WriteUInt8(byte value)
        {
            // 0 >= value <= 255
            buffer.Insert(buffer.Count, value);
            return this;
        }

        // Reads an 8bit unsigned integer.
        public byte ReadUInt8()
        {
            var offset = buffer.Count - BYTE_SIZE;
            var value = buffer[offset];
            buffer.RemoveAt(offset);
            return value;
        }
        #endregion

        #region sbyte
        // Writes an 8bit signed integer.
        public ByteBuffer WriteInt8(sbyte value)
        {
            // 0 >= value <= 255
            WriteUInt8((byte)value);
            return this;
        }

        // Reads an 8bit signed integer.
        public sbyte ReadInt8()
        {
            return (sbyte)ReadUInt8();
        }
        #endregion

        #region short
        // Writes a 16bit unsigned integer.
        public ByteBuffer WriteUInt16(ushort value)
        {
            // 0 >= value <= 65535
            var bytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian != littleEndian)
            {
                Array.Reverse(bytes);
            }
            buffer.InsertRange(buffer.Count, bytes);
            return this;
        }

        // Reads a 16bit unsigned integer.
        public ushort ReadUInt16()
        {
            var offset = buffer.Count - SHORT_SIZE;
            var bytes = buffer.GetRange(offset, SHORT_SIZE).ToArray();
            buffer.RemoveRange(offset, SHORT_SIZE);
            if (BitConverter.IsLittleEndian != littleEndian)
            {
                Array.Reverse(bytes);
            }
            return BitConverter.ToUInt16(bytes, 0);
        }
        #endregion

        #region int
        // Writes a 32bit base 128 variable-length integer.
        public int WriteVarInt32(int n)
        {
            // -2147483648 >= value <= 2147483647
            var count = CalculateVarint32(n);
            var value = (uint)n;
            var offset = 0;
            var bytes = new byte[count];
            while (value >= 0x80)
            {
                bytes[offset++] = (byte)((value & 0x7f) | 0x80);
                value = value >> 7;
            }
            bytes[offset] = (byte)value;
            buffer.InsertRange(buffer.Count, bytes);
            return count;
        }

        // Writes a zig-zag encoded (signed) 32bit base 128 variable-length integer.
        public int WriteVarInt32ZigZag(int value) => WriteVarInt32((int)ZigZagEncode32(value));

        // Writes a 32bit unsigned integer.
        public ByteBuffer WriteUInt32(uint value)
        {
            // 0 >= value <= 4294967295
            var bytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian != littleEndian)
            {
                Array.Reverse(bytes);
            }
            buffer.InsertRange(buffer.Count, bytes);
            return this;
        }

        // Reads a 32bit unsigned integer.
        public uint ReadUInt32()
        {
            var offset = buffer.Count - INT_SIZE;
            var bytes = buffer.GetRange(offset, INT_SIZE).ToArray();
            buffer.RemoveRange(offset, INT_SIZE);
            if (BitConverter.IsLittleEndian != littleEndian)
            {
                Array.Reverse(bytes);
            }
            return BitConverter.ToUInt32(bytes, 0);
        }

        // Writes a 32bit signed integer.
        public ByteBuffer WriteInt32(int value)
        {
            // –2147483648 >= value <= 2147483647
            var bytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian != littleEndian)
            {
                Array.Reverse(bytes);
            }
            buffer.InsertRange(buffer.Count, bytes);
            return this;
        }
        #endregion

        #region long
        // Writes a 64bit signed integer.
        public ByteBuffer WriteInt64(long value)
        {
            // -9223372036854775808 >= value <= 9223372036854775807
            var bytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian != littleEndian)
            {
                Array.Reverse(bytes);
            }
            buffer.InsertRange(buffer.Count, bytes);
            return this;
        }

        // Reads a 64bit signed integer.
        public long ReadInt64()
        {
            var offset = buffer.Count - LONG_SIZE;
            var bytes = buffer.GetRange(offset, LONG_SIZE).ToArray();
            buffer.RemoveRange(offset, LONG_SIZE);
            if (BitConverter.IsLittleEndian != littleEndian)
            {
                Array.Reverse(bytes);
            }
            return BitConverter.ToInt64(bytes, 0);
        }

        // Writes a 64bit unsigned integer.
        public ByteBuffer WriteUInt64(ulong value)
        {
            // 0 >= value <= 18446744073709551615
            var bytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian != littleEndian)
            {
                Array.Reverse(bytes);
            }
            buffer.InsertRange(buffer.Count, bytes);
            return this;
        }

        // Reads a 64bit unsigned integer.
        public ulong ReadUInt64()
        {
            var offset = buffer.Count - LONG_SIZE;
            var bytes = buffer.GetRange(offset, LONG_SIZE).ToArray();
            buffer.RemoveRange(offset, LONG_SIZE);
            if (BitConverter.IsLittleEndian != littleEndian)
            {
                Array.Reverse(bytes);
            }
            return BitConverter.ToUInt64(bytes, 0);
        }
        #endregion

        #region double
        // Writes a 64bit floating-point integer.
        public ByteBuffer WriteDouble(double value)
        {
            // -1.79769313486232E+308 <= value <= 1.79769313486232E+308
            var bytes = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian != littleEndian)
            {
                Array.Reverse(bytes);
            }
            buffer.InsertRange(buffer.Count, bytes);
            return this;
        }

        // Reads a 64bit floating-point integer.
        public double ReadDouble()
        {
            var offset = buffer.Count - DOUBLE_SIZE;
            var bytes = buffer.GetRange(offset, DOUBLE_SIZE).ToArray();
            buffer.RemoveRange(offset, DOUBLE_SIZE);
            if (BitConverter.IsLittleEndian != littleEndian)
            {
                Array.Reverse(bytes);
            }
            return BitConverter.ToDouble(bytes, 0);
        }
        #endregion

        #region string
        // Writes a string.
        public ByteBuffer WriteString(string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value.OrEmpty());
            WriteVarInt32(bytes.Length);
            if (bytes.Length > 0)
            {
                buffer.InsertRange(buffer.Count, bytes);
            }
            return this;
        }
        #endregion

        #region bytes
        // Writes a byte array.
        public ByteBuffer WriteBytes(byte[] value, bool writeLength = true)
        {
            var bytes = value.OrEmpty();
            if (writeLength)
            {
                WriteVarInt32(bytes.Length);
            }
            if (bytes.Length > 0)
            {
                buffer.InsertRange(buffer.Count, bytes);
            }
            return this;
        }
        #endregion

        #region array
        // Writes a generic array.
        public ByteBuffer WriteArray<T>(T[] values, Action<ByteBuffer, T> writeItem, Comparison<T> comparison = null)
        {
            WriteVarInt32(values.Length);
            if (!comparison.IsNull())
            {
                Array.Sort(values, comparison);
            }
            for (var i = 0; i < values.Length; i++)
            {
                var value = values[i];
                writeItem(this, value);
            }
            return this;
        }
        #endregion

        #region optional
        // Writes a optional generic value.
        public ByteBuffer WriteOptionalStruct<T>(T? nullable, Action<ByteBuffer, T> writeValue) where T : struct
        {
            if (!nullable.HasValue)
            {
                WriteUInt8(0);
                return this;
            }
            WriteUInt8(1);
            writeValue(this, nullable.Value);
            return this;
        }

        public ByteBuffer WriteOptionalClass<T>(T nullable, Action<ByteBuffer, T> writeValue) where T : class
        {
            if (nullable.IsNull())
            {
                WriteUInt8(0);
                return this;
            }
            WriteUInt8(1);
            writeValue(this, nullable);
            return this;
        }
        #endregion

        #region DateTime
        // Writes a DateTime struct.
        public ByteBuffer WriteDateTime(DateTime value)
        {
            var seconds = value.GetTimeFrom1Jan1970AtSeconds();
            return WriteUInt32(seconds);
        }

        // Reads a DateTime struct.
        public DateTime ReadDateTime()
        {
            var seconds = ReadUInt32();
            return TimeTool.ZeroTime().AddSeconds(seconds);
        }
        #endregion

        #region Enum
        // Writes a DateTime struct.
        public ByteBuffer WriteEnum(int value)
        {
            // ref: src/google/protobuf/wire_format_lite.h
            // ZigZag Transform:  Encodes signed integers so that they can be
            // effectively used with varint encoding.
            //
            // varint operates on unsigned integers, encoding smaller numbers into
            // fewer bytes.  If you try to use it on a signed integer, it will treat
            // this number as a very large unsigned integer, which means that even
            // small signed numbers like -1 will take the maximum number of bytes
            // (10) to encode.  ZigZagEncode() maps signed integers to unsigned
            // in such a way that those with a small absolute value will have smaller
            // encoded values, making them appropriate for encoding using varint.
            //
            //       int32 ->     uint32
            // -------------------------
            //           0 ->          0
            //          -1 ->          1
            //           1 ->          2
            //          -2 ->          3
            //         ... ->        ...
            //  2147483647 -> 4294967294
            // -2147483648 -> 4294967295
            //
            //        >> encode >>
            //        << decode <<
            WriteVarInt32ZigZag(value);
            return this;
        }
        #endregion
    }


    public class Bytes : System.Collections.Generic.List<byte> { }
}