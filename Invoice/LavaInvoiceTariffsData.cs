using Newtonsoft.Json;

namespace SKitLs.Payments.Lava.Invoice
{
    /// <summary>
    /// <b><see href="https://dev.lava.ru/api-invoice-services">[API]</see></b> Represents data related to the available tariff.
    /// </summary>
    public class LavaInvoiceTariffsData
    {
        /// <summary>
        /// Commission percentage.
        /// </summary>
        [JsonProperty("percent")]
        public double Percent { get; set; }

        /// <summary>
        /// User's commission percentage.
        /// </summary>
        [JsonProperty("user_percent")]
        public double UserPercent { get; set; }

        /// <summary>
        /// Merchant's commission percentage.
        /// </summary>
        [JsonProperty("shop_percent")]
        public double ShopPercent { get; set; }

        /// <summary>
        /// Service ID.
        /// </summary>
        [JsonProperty("service_id")]
        public string ServiceId { get; set; } = null!;

        /// <summary>
        /// Service name.
        /// </summary>
        [JsonProperty("service_name")]
        public string ServiceName { get; set; } = null!;

        /// <summary>
        /// Status of the service.
        /// </summary>
        [JsonProperty("status")]
        public int Status { get; set; }

        /// <summary>
        /// Currency.
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; } = null!;

        /// <summary>
        /// Minimum amount allowed.
        /// </summary>
        [JsonProperty("min_amount")]
        public double MinAmount { get; set; }

        /// <summary>
        /// Maximum amount allowed.
        /// </summary>
        [JsonProperty("max_amount")]
        public double MaxAmount { get; set; }

        /// <summary>
        /// Fixed commission.
        /// </summary>
        [JsonProperty("fix_commission")]
        public double FixCommission { get; set; }

        /// <summary>
        /// Discount percentage.
        /// </summary>
        [JsonProperty("discount_percent")]
        public double DiscountPercent { get; set; }

        /// <summary>
        /// Discount applied from a certain amount.
        /// </summary>
        [JsonProperty("discount_from_amount")]
        public double DiscountFromAmount { get; set; }
    }
}