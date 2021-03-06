﻿using System;
using Base.Data.Json;
using Newtonsoft.Json;


namespace Base.Data.Properties
{
    public sealed class DynamicGlobalPropertiesObject : IdObject
    {
        [JsonProperty("head_block_number")]
        public uint HeadBlockNumber { get; private set; }
        [JsonProperty("head_block_id")]
        public string HeadBlockId { get; private set; }
        [JsonProperty("time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Time { get; private set; }
        [JsonProperty("next_maintenance_time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime NextMaintenanceTime { get; private set; }
        [JsonProperty("last_budget_time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime LastBudgetTime { get; private set; }
        [JsonProperty("committee_budget")]
        public long CommitteeBudget { get; private set; }
        [JsonProperty("accounts_registered_this_interval")]
        public uint AccountsRegisteredThisInterval { get; private set; }
		[JsonProperty("dynamic_flags")]
        public uint DynamicFlags { get; private set; }
        [JsonProperty("last_irreversible_block_num")]
        public uint LastIrreversibleBlockNum { get; private set; }
		[JsonProperty("last_rand_quantity")]
		public string LastRandQuantity { get; private set; }
		[JsonProperty("extensions")]
		public object[] Extensions { get; private set; }
	}
}