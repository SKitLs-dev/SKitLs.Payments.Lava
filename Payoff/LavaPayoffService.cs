namespace SKitLs.Payments.Lava.Payoff
{
    /// <summary>
    /// Enum representing the service for withdrawal.
    /// </summary>
    public enum LavaPayoffService
    {
        /// <summary>
        /// Lava Wallet withdrawal service.
        /// </summary>
        lava_payoff,

        /// <summary>
        /// <b>[OBSOLETE]</b> QIWI withdrawal service.
        /// </summary>
        qiwi_payoff,

        /// <summary>
        /// Bank card withdrawal service.
        /// </summary>
        card_payoff,

        /// <summary>
        /// <b>[OBSOLETE]</b> Steam withdrawal service.
        /// </summary>
        steam_payoff
    }
}