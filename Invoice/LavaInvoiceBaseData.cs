using Newtonsoft.Json;

namespace SKitLs.Payments.Lava.Invoice
{
    /// <summary>
    /// Represents the base data structure for a Lava invoice. This class provides common properties shared among various types of Lava invoices
    /// (<see cref="LavaInvoiceCreateData"/>, <see cref="LavaInvoiceStatusData"/>).
    /// </summary>
    public abstract class LavaInvoiceBaseData
    {
        /// <summary>
        /// Unique identifier of the invoice in the Lava system.
        /// </summary>
        [JsonProperty("id")]
        public string InvoiceId { get; set; } = null!;

        /// <summary>
        /// Amount of the invoice.
        /// </summary>
        [JsonProperty("amount")]
        public double Amount { get; set; }

        /// <summary>
        /// Project identifier.
        /// </summary>
        [JsonProperty("shop_id")]
        public string ShopId { get; set; } = null!;

        /// <summary>
        /// Status of the invoice.
        /// </summary>
        [JsonProperty("status")]
        public LavaInvoiceStatus Status { get; set; }

        /// <summary>
        /// URL to send a hook.
        /// </summary>
        [JsonProperty("hook_url")]
        public string? HookUrl { get; set; }

        /// <summary>
        /// URL to redirect in case of successful payment.
        /// </summary>
        [JsonProperty("success_url")]
        public string? SuccessUrl { get; set; }

        /// <summary>
        /// URL to redirect in case of failed payment.
        /// </summary>
        [JsonProperty("fail_url")]
        public string? FailUrl { get; set; }

        /// <summary>
        /// Additional fields when issuing an invoice.
        /// </summary>
        [JsonProperty("custom_fields")]
        public string? CustomFields { get; set; }

        /// <summary>
        /// Payment methods available on the invoice page.
        /// </summary>
        [JsonProperty("include_service")]
        public string[]? IncludeService { get; set; }

        /// <summary>
        /// Payment methods excluded from the invoice page.
        /// </summary>
        [JsonProperty("exclude_service")]
        public string[]? ExcludeService { get; set; }
    }
}