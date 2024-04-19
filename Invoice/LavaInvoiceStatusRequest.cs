using Newtonsoft.Json;

namespace SKitLs.Payments.Lava.Invoice
{
    /// <summary>
    /// <b><see href="https://dev.lava.ru/api-invoice-status">[API]</see></b> Represents a request for the status of a Lava invoice.
    /// </summary>
    /// <param name="shopId">Project identifier.</param>
    public class LavaInvoiceStatusRequest(string shopId)
    {
        /// <summary>
        /// Project identifier.
        /// </summary>
        [JsonProperty("shopId")]
        public string ShopId { get; set; } = shopId;

        /// <summary>
        /// Unique identifier of the payment in the merchant's system.
        /// </summary>
        [JsonProperty("orderId", NullValueHandling = NullValueHandling.Ignore)]
        public string? OrderId { get; set; }

        /// <summary>
        /// Unique identifier of the invoice.
        /// </summary>
        [JsonProperty("invoiceId", NullValueHandling = NullValueHandling.Ignore)]
        public string? InvoiceId { get; set; }

        /// <summary>
        /// Creates a new instance of <see cref="LavaInvoiceStatusRequest"/> based on the provided shop ID and order ID.
        /// </summary>
        /// <param name="shopId">The shop ID.</param>
        /// <param name="orderId">The order ID in the merchant's system.</param>
        /// <returns>A new instance of <see cref="LavaInvoiceStatusRequest"/> initialized with the provided shop ID and order ID.</returns>
        public static LavaInvoiceStatusRequest FromOrderId(string shopId, string orderId) => new(shopId) { OrderId = orderId };

        /// <summary>
        /// Creates a new instance of <see cref="LavaInvoiceStatusRequest"/> based on the provided shop ID and invoice ID.
        /// </summary>
        /// <param name="shopId">The shop ID.</param>
        /// <param name="invoiceId">The invoice ID.</param>
        /// <returns>A new instance of <see cref="LavaInvoiceStatusRequest"/> initialized with the provided shop ID and invoice ID.</returns>
        public static LavaInvoiceStatusRequest FromInvoiceId(string shopId, string invoiceId) => new(shopId) { InvoiceId = invoiceId };
    }
}