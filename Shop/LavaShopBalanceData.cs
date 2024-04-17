using Newtonsoft.Json;

namespace SKitLs.Payments.Lava.Shop
{
    /// <summary>
    /// <b><see href="https://dev.lava.ru/shop-balance">[API]</see></b> Represents shop balance information.
    /// </summary>
    public class LavaShopBalanceData
    {
        /// <summary>
        /// The shop's total balance.
        /// </summary>
        [JsonProperty("balance")]
        public double Balance { get; set; }

        /// <summary>
        /// The shop's active balance.
        /// </summary>
        [JsonProperty("active_balance")]
        public double ActiveBalance { get; set; }

        /// <summary>
        /// The shop's frozen balance.
        /// </summary>
        [JsonProperty("freeze_balance")]
        public double FreezeBalance { get; set; }
    }
}