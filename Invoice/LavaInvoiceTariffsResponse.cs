using SKitLs.Payments.Lava.Common;

namespace SKitLs.Payments.Lava.Invoice
{
    /// <summary>
    /// <b><see href="https://dev.lava.ru/api-invoice-services">[API]</see></b> Represents the response from the server containing information about payment services and tariffs data.
    /// </summary>
    public class LavaInvoiceTariffsResponse : LavaResponseBase<List<LavaInvoiceTariffsData>>
    { }
}