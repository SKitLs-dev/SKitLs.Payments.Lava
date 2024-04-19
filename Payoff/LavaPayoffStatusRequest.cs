using Newtonsoft.Json;

namespace SKitLs.Payments.Lava.Payoff
{
    /// <summary>
    /// <b><see href="https://dev.lava.ru/business-payoff-info">[API]</see></b> Represents a request to get Lava payoff status.
    /// </summary>
    /// <param name="shopId">The project identifier.</param>
    public class LavaPayoffStatusRequest(string shopId)
    {
        /// <summary>
        /// Gets or sets the unique identifier of the project.
        /// </summary>
        [JsonProperty("shopId")]
        public string ShopId { get; set; } = shopId;

        /// <summary>
        /// The unique identifier of the payment in the merchant's system.
        /// </summary>
        [JsonProperty("orderId", NullValueHandling = NullValueHandling.Ignore)]
        public string? OrderId { get; set; }

        /// <summary>
        /// The unique identifier of the payout.
        /// </summary>
        [JsonProperty("payoffId", NullValueHandling = NullValueHandling.Ignore)]
        public string? PayoffId { get; set; }

        /// <summary>
        /// Creates a new instance of <see cref="LavaPayoffStatusRequest"/> based on the provided shop ID and order ID.
        /// </summary>
        /// <param name="shopId">The shop ID.</param>
        /// <param name="orderId">The order ID in the merchant's system.</param>
        /// <returns>A new instance of <see cref="LavaPayoffStatusRequest"/> initialized with the provided shop ID and order ID.</returns>
        public static LavaPayoffStatusRequest FromOrderId(string shopId, string orderId) => new(shopId) { OrderId = orderId };

        /// <summary>
        /// Creates a new instance of <see cref="LavaPayoffStatusRequest"/> based on the provided shop ID and payoffId ID.
        /// </summary>
        /// <param name="shopId">The shop ID.</param>
        /// <param name="payoffId">The payoffId ID.</param>
        /// <returns>A new instance of <see cref="LavaPayoffStatusRequest"/> initialized with the provided shop ID and payoffId ID.</returns>
        public static LavaPayoffStatusRequest FromPayoffId(string shopId, string payoffId) => new(shopId) { PayoffId = payoffId };
    }
}