using Newtonsoft.Json;
using SKitLs.Payments.Lava.Extensions;

namespace SKitLs.Payments.Lava.Invoice
{
    /// <summary>
    /// <b><see href="https://dev.lava.ru/api-invoice-status">[API]</see></b> Represents data related to a Lava invoice (getting status).
    /// </summary>
    /// <seealso cref="LavaInvoiceBaseData"/>
    public class LavaInvoiceStatusData : LavaInvoiceBaseData
    {
        /// <summary>
        /// Expiration date and time of the invoice.
        /// </summary>
        [JsonProperty("expire")]
        [JsonConverter(typeof(LavaDateTimeConverter))]
        public DateTime Expire { get; set; }

        /// <summary>
        /// Unique identifier of the payment in the merchant's system.
        /// </summary>
        [JsonProperty("orderId")]
        public string OrderId { get; set; } = null!;
    }
}