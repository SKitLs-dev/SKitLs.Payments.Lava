using Newtonsoft.Json;

namespace SKitLs.Payments.Lava.Invoice
{
    /// <summary>
    /// <b><see href="https://dev.lava.ru/api-invoice-create">[API]</see></b> Represents a request object for creating a Lava invoice.
    /// </summary>
    /// <param name="sum">Invoice amount.</param>
    /// <param name="orderId">Unique identifier of the payment in the merchant's system.</param>
    /// <param name="shopId">Project identifier.</param>
    public class LavaInvoiceCreateRequest(double sum, string orderId, string shopId)
    {
        /// <summary>
        /// Amount of the invoice.
        /// </summary>
        [JsonProperty("sum")]
        public double Sum { get; set; } = sum;

        /// <summary>
        /// Unique identifier of the payment in the merchant's system.
        /// </summary>
        [JsonProperty("orderId")]
        public string OrderId { get; set; } = orderId;
        
        /// <summary>
        /// Project identifier.
        /// </summary>
        [JsonProperty("shopId")]
        public string ShopId { get; set; } = shopId;
        
        /// <summary>
        /// Time in minutes before the invoice expires. Default is 300 minutes (5 hours).
        /// </summary>
        [JsonProperty("expire")]
        public int ExpireMinutes { get; set; } = 300;

        /// <summary>
        /// URL to send a hook.
        /// </summary>
        [JsonProperty("hookUrl")]
        public string? HookUrl { get; set; }

        /// <summary>
        /// URL to redirect in case of successful payment.
        /// </summary>
        [JsonProperty("successUrl")]
        public string? SuccessUrl { get; set; }

        /// <summary>
        /// URL to redirect in case of failed payment.
        /// </summary>
        [JsonProperty("failUrl")]
        public string? FailUrl { get; set; }

        /// <summary>
        /// Additional fields when issuing an invoice.
        /// </summary>
        [JsonProperty("customFields")]
        public string? CustomFields { get; set; }

        /// <summary>
        /// Comment for the issued invoice.
        /// </summary>
        [JsonProperty("comment")]
        public string? Comment { get; set; }

        /// <summary>
        /// Payment methods available on the invoice page.
        /// </summary>
        [JsonProperty("includeService")]
        public List<string>? IncludeService { get; set; } = [ "card", "sbp" ];
    }
}