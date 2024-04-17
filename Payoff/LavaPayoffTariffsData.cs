using Newtonsoft.Json;

namespace SKitLs.Payments.Lava.Payoff
{
    /// <summary>
    /// <b><see href="https://dev.lava.ru/business-payoff-tariffs">[API]</see></b> Represents data related to the payoff tariffs data.
    /// </summary>
    public class LavaPayoffTariffsData
    {
        /// <summary>
        /// The commission percentage.
        /// </summary>
        [JsonProperty("percent")]
        public double Percent { get; set; }

        /// <summary>
        /// The minimum payoff sum.
        /// </summary>
        [JsonProperty("min_sum")]
        public double MinSum { get; set; }

        /// <summary>
        /// The maximum payoff sum.
        /// </summary>
        [JsonProperty("max_sum")]
        public double MaxSum { get; set; }

        /// <summary>
        /// The payment service type.
        /// </summary>
        [JsonProperty("service")]
        public LavaPayoffService Service { get; set; }

        /// <summary>
        /// The fixed value <i>(No additional info provided)</i>.
        /// </summary>
        [JsonProperty("fix")]
        public double Fix { get; set; }

        /// <summary>
        /// The title of the payment service.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; } = null!;

        /// <summary>
        /// The payment service currency.
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; } = null!;
    }
}