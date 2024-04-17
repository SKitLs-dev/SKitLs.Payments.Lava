using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SKitLs.Payments.Lava.Common;

namespace SKitLs.Payments.Lava.Invoice
{
    /// <summary>
    /// <b><see href="https://dev.lava.ru/api-invoice-create">[API]</see></b> Represents a response from the Lava invoice API.
    /// </summary>
    public class LavaInvoiceCreateResponse : LavaResponseBase<LavaInvoiceCreateData>
    { }
}