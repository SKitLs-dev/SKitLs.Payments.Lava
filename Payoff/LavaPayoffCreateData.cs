using Newtonsoft.Json;

namespace SKitLs.Payments.Lava.Payoff
{
    /// <summary>
    /// <b><see href="https://dev.lava.ru/business-payoff-create">[API]</see></b> Represents data related to a created Lava payoff.
    /// </summary>
    public class LavaPayoffCreateData
    {
        /// <summary>
        /// Unique identifier of the payoff.
        /// </summary>
        [JsonProperty("payoff_id")]
        public string PayoffId { get; set; } = null!;

        /// <summary>
        /// Status of the payoff.
        /// </summary>
        [JsonProperty("payoff_status")]
        public LavaPayoffStatus PayoffStatus { get; set; }
    }
}