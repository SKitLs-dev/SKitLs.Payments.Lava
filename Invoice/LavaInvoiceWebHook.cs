using Newtonsoft.Json;
using SKitLs.Payments.Lava.Extensions;

namespace SKitLs.Payments.Lava.Invoice
{
    /// <summary>
    /// <b><see href="https://dev.lava.ru/business-webhook">[API]</see></b> Represents information about an invoice WebHook.
    /// </summary>
    public class LavaInvoiceWebHook
    {
        /// <summary>
        /// The invoice ID.
        /// </summary>
        [JsonProperty("invoice_id")]
        public string InvoiceId { get; set; } = null!;

        /// <summary>
        /// The order ID.
        /// </summary>
        [JsonProperty("order_id")]
        public string OrderId { get; set; } = null!;

        /// <summary>
        /// The status of the invoice.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; } = null!;

        /// <summary>
        /// The time when the invoice was paid.
        /// </summary>
        [JsonProperty("pay_time")]
        [JsonConverter(typeof(LavaDateTimeConverter))]
        public DateTime PayTime { get; set; }

        /// <summary>
        /// The amount of the invoice.
        /// </summary>
        [JsonProperty("amount")]
        public double Amount { get; set; }

        /// <summary>
        /// Additional custom fields.
        /// </summary>
        [JsonProperty("custom_fields")]
        public string? CustomFields { get; set; }

        /// <summary>
        /// The amount credited to the balance.
        /// </summary>
        [JsonProperty("credited")]
        public double Credited { get; set; }
    }
}