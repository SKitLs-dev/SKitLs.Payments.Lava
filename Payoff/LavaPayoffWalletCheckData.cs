using Newtonsoft.Json;

namespace SKitLs.Payments.Lava.Payoff
{
    /// <summary>
    /// <b><see href="https://dev.lava.ru/business-payoff-wallet-check">[API]</see></b> Represents the data returned from a Lava payoff wallet check.
    /// </summary>
    public class LavaPayoffWalletCheckData
    {
        /// <summary>
        /// The status of the wallet check.
        /// </summary>
        /// <remarks>
        /// <see langword="true"/> if the credentials are valid; otherwise <see langword="false"/>.
        /// </remarks>
        [JsonProperty("status")]
        public bool Status { get; set; }
    }
}