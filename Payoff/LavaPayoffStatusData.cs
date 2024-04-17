using Newtonsoft.Json;

namespace SKitLs.Payments.Lava.Payoff
{
    /// <summary>
    /// <b><see href="https://dev.lava.ru/business-payoff-info">[API]</see></b> Represents data related to the status of a payoff.
    /// </summary>
    public class LavaPayoffStatusData
    {
        /// <summary>
        /// Unique identifier of the payment.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; } = null!;

        /// <summary>
        /// Order ID associated with the payment.
        /// </summary>
        [JsonProperty("orderId")]
        public string OrderId { get; set; } = null!;

        /// <summary>
        /// Status of the payment.
        /// </summary>
        [JsonProperty("status")]
        public LavaPayoffStatus Status { get; set; }

        /// <summary>
        /// Wallet identifier.
        /// </summary>
        [JsonProperty("wallet")]
        public string Wallet { get; set; } = null!;

        /// <summary>
        /// Service used for the payment.
        /// </summary>
        [JsonProperty("service")]
        public LavaPayoffService Service { get; set; }

        /// <summary>
        /// Amount paid from the shop.
        /// </summary>
        [JsonProperty("amountPay")]
        public double AmountPay { get; set; }

        /// <summary>
        /// Commission for the payment.
        /// </summary>
        [JsonProperty("commission")]
        public double Commission { get; set; }

        /// <summary>
        /// Amount received by the receiver.
        /// </summary>
        [JsonProperty("amountReceive")]
        public double AmountReceive { get; set; }

        /// <summary>
        /// Number of payoff attempts made.
        /// </summary>
        [JsonProperty("tryCount")]
        public int TryCount { get; set; }

        /// <summary>
        /// Error message, if any.
        /// </summary>
        [JsonProperty("errorMessage")]
        public string? ErrorMessage { get; set; }
    }
}