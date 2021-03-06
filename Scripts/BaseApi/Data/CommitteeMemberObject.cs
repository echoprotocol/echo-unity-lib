﻿using Newtonsoft.Json;


namespace Base.Data
{
    public sealed class CommitteeMemberObject : IdObject
    {
        [JsonProperty("committee_member_account")]
        public SpaceTypeId CommitteeMemberAccount { get; private set; }
        [JsonProperty("vote_id")]
        public VoteId Vote { get; private set; }
        [JsonProperty("total_votes")]
        public ulong TotalVotes { get; private set; }
        [JsonProperty("url")]
        public string Url { get; private set; }
        [JsonProperty("extensions")]
        public object[] Extensions { get; private set; }
    }
}