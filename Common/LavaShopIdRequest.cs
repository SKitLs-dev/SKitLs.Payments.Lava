using Newtonsoft.Json;

namespace SKitLs.Payments.Lava.Common
{
    /// <summary>
    /// Represents a request object requiring only the ShopId parameter.
    /// </summary>
    /// <param name="shopId">Project identifier.</param>
    public class LavaShopIdRequest(string shopId)
    {
        /// <summary>
        /// The project identifier.
        /// </summary>
        [JsonProperty("shopId")]
        public string ShopId { get; set; } = shopId;
    }
}