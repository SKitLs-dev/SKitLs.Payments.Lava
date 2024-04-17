namespace SKitLs.Payments.Lava.Payoff
{
    /// <summary>
    /// Represents the status of the payoff.
    /// </summary>
    public enum LavaPayoffStatus
    {
        /// <summary>
        /// Represents a created status for a withdrawal.
        /// </summary>
        Created,

        /// <summary>
        /// Represents a successful withdrawal.
        /// </summary>
        Success,

        /// <summary>
        /// Represents a rejected withdrawal.
        /// </summary>
        Rejected
    }
}