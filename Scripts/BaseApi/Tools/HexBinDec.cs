using System;
using System.Collections.Generic;
using System.Text;
using CustomTools.Extensions.Core;
using CustomTools.Extensions.Core.Array;


namespace Tools.HexBinDec
{
    public static class Extensions
    {
        private readonly static Dictionary<string, byte> hexLibrary = new Dictionary<string, byte>();
        private readonly static Dictionary<string, byte> binaryLibrary = new Dictionary<string, byte>();


        private static Dictionary<string, byte> HexLibrary
        {
            get
            {
                if (hexLibrary.Count == 0)
                {
                    for (var i = 0; i < 256; i++)
                    {
                        hexLibrary.Add(Convert.ToString((byte)i, 16).PadLeft(2, '0'), (byte)i);
                    }
                }
                return hexLibrary;
            }
        }

        private static Dictionary<string, byte> BinaryLibrary
        {
            get
            {
                if (binaryLibrary.Count == 0)
                {
                    for (var i = 0; i < 256; i++)
                    {
                        binaryLibrary.Add(Convert.ToString((byte)i, 2).PadLeft(8, '0'), (byte)i);
                    }
                }
                return binaryLibrary;
            }
        }

        public static string ToHexString(this byte[] data, char? separator = null)
        {
            if (data.IsNullOrEmpty())
            {
                return string.Empty;
            }
            var builder = new StringBuilder();
            for (var i = 0; i < data.Length; i++)
            {
                ((separator.HasValue && (i > 0)) ? builder.Append(separator.Value) : builder).Append(Convert.ToString(data[i], 16).PadLeft(2, '0'));
            }
            return builder.ToString();
        }

        public static string ToDecimalString(this byte[] data, char? separator = null)
        {
            if (data.IsNullOrEmpty())
            {
                return string.Empty;
            }
            var builder = new StringBuilder();
            for (var i = 0; i < data.Length; i++)
            {
                ((separator.HasValue && (i > 0)) ? builder.Append(separator.Value) : builder).Append(Convert.ToString(data[i], 10));
            }
            return builder.ToString();
        }

        public static string ToBinaryString(this byte[] data, char? separator = null)
        {
            if (data.IsNullOrEmpty())
            {
                return string.Empty;
            }
            var builder = new StringBuilder();
            for (var i = 0; i < data.Length; i++)
            {
                ((separator.HasValue && (i > 0)) ? builder.Append(separator.Value) : builder).Append(Convert.ToString(data[i], 2).PadLeft(8, '0'));
            }
            return builder.ToString();
        }

        public static byte[] FromHex2Data(this string hexString, uint? fixedSize = null)
        {
            if (hexString.IsNullOrEmpty())
            {
                return new byte[0];
            }
            if (hexString.Length % 2 != 0)
            {
                throw new ArgumentException("The binary key cannot have an odd number of digits: " + hexString);
            }
            var result = new List<byte>();
            for (var i = 0; i < hexString.Length; i += 2)
            {
                result.Add(HexLibrary[hexString.Substring(i, 2)]);
            }
            if (fixedSize.HasValue && !result.Count.Equals(fixedSize.Value))
            {
                while (result.Count > fixedSize.Value)
                {
                    result.RemoveAt(0);
                }
                while (result.Count < fixedSize.Value)
                {
                    result.Insert(0, 0x00);
                }
            }
            return result.ToArray();
        }

        public static string FromHex2Chars(this string hexString)
        {
            var data = hexString.FromHex2Data();
            var builder = new StringBuilder();
            for (var i = 0; i < data.Length; i++)
            {
                if (data[i] > 0)
                {
                    builder.Append((char)data[i]);
                }
            }
            return builder.ToString();
        }

        public static byte[] FromBinary2Data(string binaryString)
        {
            if (binaryString.IsNullOrEmpty())
            {
                return new byte[0];
            }
            if (binaryString.Length % 8 != 0)
            {
                throw new ArgumentException("The binary key cannot have an odd number of digits: " + binaryString);
            }
            var result = new List<byte>();
            for (var i = 0; i < binaryString.Length; i += 8)
            {
                result.Add(BinaryLibrary[binaryString.Substring(i, 8)]);
            }
            return result.ToArray();
        }
    }
}