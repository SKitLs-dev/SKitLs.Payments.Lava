using Newtonsoft.Json;
using SKitLs.Payments.Lava.Extensions;

namespace SKitLs.Payments.Lava.Invoice
{
    /// <summary>
    /// <b><see href="https://dev.lava.ru/api-invoice-create">[API]</see></b> Represents data related to a Lava invoice (creating invoice).
    /// </summary>
    /// <seealso cref="LavaInvoiceBaseData"/>
    public class LavaInvoiceCreateData : LavaInvoiceBaseData
    {
        /// <summary>
        /// Expiration date and time of the invoice.
        /// </summary>
        [JsonProperty("expired")]
        [JsonConverter(typeof(LavaDateTimeConverter))]
        public DateTime Expired { get; set; }

        ///// <summary>
        ///// Status of the invoice <i>(No more info provided)</i>.
        ///// </summary>
        //[JsonProperty("status")]
        //public int Status { get; set; }

        /// <summary>
        /// URL of the invoice.
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; } = null!;

        /// <summary>
        /// Comment for the issued invoice.
        /// </summary>
        [JsonProperty("comment")]
        public string? Comment { get; set; }

        /// <summary>
        /// Name of the merchant.
        /// </summary>
        [JsonProperty("merchantName")]
        public string MerchantName { get; set; } = null!;
    }
}