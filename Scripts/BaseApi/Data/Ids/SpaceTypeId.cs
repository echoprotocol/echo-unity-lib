using System;
using System.Text;
using Base.Data.Json;
using Buffers;
using CustomTools.Extensions.Core;
using Newtonsoft.Json;


namespace Base.Data
{
    [JsonConverter(typeof(SpaceTypeEnumConverter))]
    public enum SpaceType
    {
        Unknown,
        Base,
        Account,
        Asset,
        ForceSettlement,
        CommitteeMember,
        LimitOrder,
        CallOrder,
        Custom,
        Proposal,
        OperationHistory,
        WithdrawPermission,
        VestingBalance,
        Balance,
        Contract,
        ContractResult,
        BlockResult,
        ETHAddress,
        DepositETH,
        WithdrawETH,
        GlobalProperties,
        DynamicGlobalProperties,
        Reserved_0,
        AssetDynamicData,
        AssetBitassetData,
        AccountBalance,
        AccountStatistics,
        Transaction,
        BlockSummary,
        AccountTransactionHistory,
        ChainProperty,
        BudgetRecord,
        SpecialAuthority,
        Buyback,
        CollateralBid,
        ContractBalance,
        ContractHistory,
        ContractStatistics,
        AccountAddress,
        ContractPool
    }


    [JsonConverter(typeof(SpaceTypeIdConverter))]
    public sealed class SpaceTypeId : SerializableObject, ISerializeToBuffer, IEquatable<SpaceTypeId>, IComparable<SpaceTypeId>
    {
        public readonly static SpaceTypeId EMPTY = new SpaceTypeId();

        private const char TYPE_SEPARATOR = '.';

        private readonly byte space = 0;
        private readonly byte type = 0;
        private readonly uint id = 0;


        public static SpaceTypeId[] CreateMany(SpaceType spaceType, uint[] ids)
        {
            var result = new SpaceTypeId[ids.Length];
            for (var i = 0; i < ids.Length; i++)
            {
                result[i] = CreateOne(spaceType, ids[i]);
            }
            return result;
        }

        public static SpaceTypeId CreateOne(SpaceType spaceType, uint id = uint.MinValue) => Create(ToString(spaceType, id));

        public static SpaceTypeId Create(string spaceTypeId)
        {
            if (spaceTypeId.IsNullOrEmpty())
            {
                throw new NullReferenceException();
            }
            var parts = spaceTypeId.Split(TYPE_SEPARATOR);
            if (parts.Length != 3)
            {
                throw new FormatException(spaceTypeId);
            }
            return new SpaceTypeId(
                Convert.ToByte(parts[0]),
                Convert.ToByte(parts[1]),
                Convert.ToUInt32(parts[2])
            );
        }

        private SpaceTypeId() { }

        private SpaceTypeId(byte space, byte type, uint id)
        {
            this.space = space;
            this.type = type;
            this.id = id;
        }

        public uint Id => id;

        public SpaceType SpaceType => SpaceTypeEnumConverter.ConvertFrom(space.ToString() + TYPE_SEPARATOR + type.ToString());

        public override string Serialize()
        {
            var builder = new StringBuilder();
            builder.Append(space);
            builder.Append(TYPE_SEPARATOR);
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
            if (!(obj is SpaceTypeId))
            {
                return false;
            }
            return Equals((SpaceTypeId)obj);
        }

        public bool Equals(SpaceTypeId spaceTypeId) => ToString().Equals(spaceTypeId.ToString());

        public int CompareTo(SpaceTypeId other)
        {
            return string.Compare(ToString(), other.ToString(), StringComparison.Ordinal);
        }

        public static int Compare(SpaceTypeId a, SpaceTypeId b) => a.CompareTo(b);

        public static string ToString(SpaceType spaceType, uint id = uint.MinValue)
        {
            return SpaceTypeEnumConverter.ConvertTo(spaceType) + TYPE_SEPARATOR + id;
        }

        public static string[] ToStrings(SpaceType spaceType, uint[] ids)
        {
            var result = new string[ids.Length];
            for (var i = 0; i < ids.Length; i++)
            {
                result[i] = ToString(spaceType, ids[i]);
            }
            return result;
        }

        public ByteBuffer ToBuffer(ByteBuffer buffer = null)
        {
            buffer = buffer ?? new ByteBuffer(ByteBuffer.LITTLE_ENDING);
            buffer.WriteVarInt32((int)id);
            return buffer;
        }
    }


    public static class SpaceTypeIdExtensions
    {
        public static bool IsNullOrEmpty(this SpaceTypeId sti) => sti.IsNull() || SpaceTypeId.EMPTY.Equals(sti);
    }
}