using Newtonsoft.Json;
using SKitLs.Payments.Lava.Extensions;

namespace SKitLs.Payments.Lava.Payoff
{
    /// <summary>
    /// <b><see href="https://dev.lava.ru/business-payoff-create">[API]</see></b> Represents a request to create a Lava payoff.
    /// </summary>
    /// <param name="amount">The amount to withdraw.</param>
    /// <param name="orderId">The unique identifier of the payment in the merchant's system.</param>
    /// <param name="shopId">The project identifier.</param>
    /// <param name="service">The service for withdrawal.</param>
    /// <param name="walletTo">The destination wallet for the withdrawal.</param>
    /// <param name="subtract">The source for subtracting commission: 0 - from the amount (default) or 1 - from the shop.</param>
    public class LavaPayoffCreateRequest(double amount, string orderId, string shopId, LavaPayoffService service, string walletTo, int subtract = 0)
    {
        /// <summary>
        /// The amount to withdraw.
        /// </summary>
        [JsonProperty("amount")]
        public double Amount { get; set; } = amount;

        /// <summary>
        /// The unique identifier of the payment in the merchant's system.
        /// </summary>
        [JsonProperty("orderId")]
        public string OrderId { get; set; } = orderId;

        /// <summary>
        /// The project identifier.
        /// </summary>
        [JsonProperty("shopId")]
        public string ShopId { get; set; } = shopId;

        /// <summary>
        /// The URL to send a hook.
        /// </summary>
        [JsonProperty("hookUrl")]
        public string? HookUrl { get; set; }

        /// <summary>
        /// The service for withdrawal.
        /// </summary>
        [JsonProperty("service")]
        public LavaPayoffService Service { get; set; } = service;

        /// <summary>
        /// The destination wallet for the withdrawal.
        /// </summary>
        /// <remarks>
        /// Pass Lava Wallet id or Card Number.
        /// </remarks>
        [JsonProperty("walletTo")]
        public string WalletTo { get; set; } = walletTo;

        /// <summary>
        /// The source for subtracting commission: 0 - from the amount (default) or 1 - from the shop.
        /// </summary>
        [JsonProperty("subtract")]
        public int Subtract { get; set; } = subtract;

        /// <summary>
        /// Creates a new instance of <see cref="LavaPayoffCreateRequest"/> with Lava Wallet as the withdrawal destination.
        /// </summary>
        /// <param name="amount">The amount to withdraw.</param>
        /// <param name="orderId">The unique identifier of the payment in the merchant's system.</param>
        /// <param name="shopId">The project identifier.</param>
        /// <param name="walletId">The Lava Wallet ID.</param>
        /// <returns>A new instance of <see cref="LavaPayoffCreateRequest"/> initialized with the provided parameters.</returns>
        /// <exception cref="ArgumentException">Thrown when the input <paramref name="walletId"/> does not match the R00000000 pattern.</exception>
        public static LavaPayoffCreateRequest ToLavaWallet(double amount, string orderId, string shopId, string walletId) => R.LavaWalletRegex().IsMatch(walletId)
            ? new LavaPayoffCreateRequest(amount, orderId, shopId, LavaPayoffService.lava_payoff, walletId)
            : throw new ArgumentException($"Input {nameof(walletId)} does not match R00000000 pattern", nameof(walletId));

        /// <summary>
        /// Creates a new instance of <see cref="LavaPayoffCreateRequest"/> with a bank card as the withdrawal destination.
        /// </summary>
        /// <param name="amount">The amount to withdraw.</param>
        /// <param name="orderId">The unique identifier of the payment in the merchant's system.</param>
        /// <param name="shopId">The project identifier.</param>
        /// <param name="bankCard">The bank card number.</param>
        /// <returns>A new instance of <see cref="LavaPayoffCreateRequest"/> initialized with the provided parameters.</returns>
        /// <exception cref="ArgumentException">Thrown when the input <paramref name="bankCard"/> does not match the 0000000000000000 pattern.</exception>
        public static LavaPayoffCreateRequest ToBankCard(double amount, string orderId, string shopId, string bankCard) => R.BankCardRegex().IsMatch(R.NoSpaceRegex().Replace(bankCard, ""))
            ? new LavaPayoffCreateRequest(amount, orderId, shopId, LavaPayoffService.card_payoff, bankCard)
            : throw new ArgumentException($"Input {bankCard} does not match 0000000000000000 pattern", nameof(bankCard));
    }
}