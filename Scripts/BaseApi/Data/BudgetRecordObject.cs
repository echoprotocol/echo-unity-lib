using System;
using Base.Data.Json;
using Newtonsoft.Json;


namespace Base.Data
{
    // id "2.11.x"
    public sealed class BudgetRecordObject : IdObject
    {
        [JsonProperty("time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Time { get; private set; }
        [JsonProperty("record")]
        public BudgetRecordData Record { get; private set; }
    }


    public sealed class BudgetRecordData : SerializableObject
    {
        [JsonProperty("time_since_last_budget")]
        public ulong TimeSinceLastBudget { get; private set; }
        [JsonProperty("from_initial_reserve")]
        public long FromInitialReserve { get; private set; }
        [JsonProperty("from_accumulated_fees")]
        public long FromAccumulatedFees { get; private set; }
        [JsonProperty("from_unused_witness_budget")]
        public long FromUnusedWitnessBudget { get; private set; }
        [JsonProperty("requested_witness_budget")]
        public long RequestedWitnessBudget { get; private set; }
        [JsonProperty("total_budget")]
        public long TotalBudget { get; private set; }
        [JsonProperty("witness_budget")]
        public long WitnessBudget { get; private set; }
        [JsonProperty("worker_budget")]
        public long WorkerBudget { get; private set; }
        [JsonProperty("leftover_worker_funds")]
        public long LeftoverWorkerFunds { get; private set; }
        [JsonProperty("supply_delta")]
        public long SupplyDelta { get; private set; }
    }
}