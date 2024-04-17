namespace SKitLs.Payments.Lava.Invoice
{
    /// <summary>
    /// Represents the status of a Lava invoice (<see cref="LavaInvoiceBaseData"/>).
    /// </summary>
    public enum LavaInvoiceStatus
    {
        /// <summary>
        /// The invoice has been created.
        /// </summary>
        Created = 1,

        /// <summary>
        /// The payment for the invoice was successful.
        /// </summary>
        Success = 2,

        /// <summary>
        /// The invoice has expired.
        /// </summary>
        Expired = 3,
    }
}