using System;
using System.Text;
using Base.Config;
using Base.Data.Json;
using Buffers;
using CustomTools.Extensions.Core;
using Newtonsoft.Json;


namespace Base.Data
{
    [JsonConverter(typeof(VoteIdConverter))]
    public sealed class VoteId : SerializableObject, ISerializeToBuffer, IEquatable<VoteId>, IComparable<VoteId>
    {
        public readonly static VoteId EMPTY = new VoteId();

        private const char TYPE_SEPARATOR = ':';

        private readonly byte type = 0;
        private readonly uint id = 0;


        public static VoteId Create(ChainTypes.VoteType voteType, uint id = uint.MinValue) => Create(ToString(voteType, id));

        public static VoteId Create(string voteId)
        {
            if (voteId.IsNullOrEmpty())
            {
                throw new NullReferenceException();
            }
            var parts = voteId.Split(TYPE_SEPARATOR);
            if (parts.Length != 2)
            {
                throw new FormatException(voteId);
            }
            return new VoteId(
                Convert.ToByte(parts[0]),
                Convert.ToUInt32(parts[1])
            );
        }

        private VoteId() { }

        private VoteId(byte type, uint id)
        {
            this.type = type;
            this.id = id;
        }

        public uint ToUintId => id;

        public ChainTypes.VoteType VoteType => (ChainTypes.VoteType)type;

        public override string Serialize()
        {
            var builder = new StringBuilder();
            builder.Append(type);
            builder.Append(TYPE_SEPARATOR);
            builder.Append(id);
            return builder.ToString();
        }

        public override int GetHashCode() => ToString().GetHashCode();

        public override bool Equals(object obj)
        {
            if (this == obj)
            {
                return true;
            }
            if (!(obj is VoteId))
            {
                return false;
            }
            return Equals((VoteId)obj);
        }

        public bool Equals(VoteId voteId) => ToString().Equals(voteId.ToString());

        public static string ToString(ChainTypes.VoteType voteType, uint id)
        {
            return ((byte)voteType).ToString() + TYPE_SEPARATOR + id;
        }

        public static string[] ToStrings(ChainTypes.VoteType voteType, uint[] ids)
        {
            var result = new string[ids.Length];
            for (var i = 0; i < ids.Length; i++)
            {
                result[i] = ToString(voteType, ids[i]);
            }
            return result;
        }

        public ByteBuffer ToBuffer(ByteBuffer buffer = null)
        {
            buffer = buffer ?? new ByteBuffer(ByteBuffer.LITTLE_ENDING);
            buffer.WriteUInt32(id << 8 | type);
            return buffer;
        }

        public int CompareTo(VoteId other) => (int)id - (int)other.id;

        public static int Compare(VoteId a, VoteId b) => a.CompareTo(b);
    }


    public static class VoteIdExtensions
    {
        public static bool IsNullOrEmpty(this VoteId vi) => vi.IsNull() || VoteId.EMPTY.Equals(vi);
    }
}