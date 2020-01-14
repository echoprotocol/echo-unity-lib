using Buffers;
using CustomTools.Extensions.Core;
using Newtonsoft.Json;


namespace Base.Data.Accounts
{
    public sealed class AccountOptionsData : SerializableObject, ISerializeToBuffer
    {
        [JsonProperty("voting_account")]
        public SpaceTypeId VotingAccount { get; private set; }
        [JsonProperty("delegating_account")]
        public SpaceTypeId DelegatingAccount { get; private set; }
        [JsonProperty("delegate_share")]
        public ushort DelegateShare { get; private set; }
        [JsonProperty("num_committee")]
        public ushort NumCommittee { get; private set; }
        [JsonProperty("votes")]
        public VoteId[] Votes { get; private set; }
        [JsonProperty("extensions")]
        public object[] Extensions { get; private set; }

        public ByteBuffer ToBuffer(ByteBuffer buffer = null)
        {
            buffer = buffer ?? new ByteBuffer(ByteBuffer.LITTLE_ENDING);
            VotingAccount.ToBuffer(buffer);
            DelegatingAccount.ToBuffer(buffer);
            buffer.WriteUInt16(DelegateShare);
            buffer.WriteUInt16(NumCommittee);
            buffer.WriteArray(Votes, (b, item) =>
            {
                if (!item.IsNull())
                {
                    item.ToBuffer(b);
                }
            }, VoteId.Compare);
            buffer.WriteArray(Extensions, (b, item) =>
            {
                if (!item.IsNull())
                {
                    ;
                }
            });
            return buffer;
        }
    }
}