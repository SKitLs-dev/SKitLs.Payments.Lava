using Newtonsoft.Json;
using SKitLs.Payments.Lava.Extensions;

namespace SKitLs.Payments.Lava.Payoff
{
    /// <summary>
    /// <b><see href="https://dev.lava.ru/business-payoff-wallet-check">[API]</see></b> Represents a request to check receiver wallet.
    /// </summary>
    /// <param name="shopId">The project identifier.</param>
    /// <param name="service">The service for withdrawal.</param>
    /// <param name="walletTo">The destination wallet for the withdrawal.</param>
    public class LavaPayoffWalletCheckRequest(string shopId, LavaPayoffService service, string walletTo)
    {
        /// <summary>
        /// The project identifier.
        /// </summary>
        [JsonProperty("shopId")]
        public string ShopId { get; set; } = shopId;

        /// <summary>
        /// The payment service type.
        /// </summary>
        [JsonProperty("service")]
        public LavaPayoffService Service { get; set; } = service;

        /// <summary>
        /// The Lava Wallet id or bank account number.
        /// </summary>
        [JsonProperty("walletTo")]
        public string WalletTo { get; set; } = walletTo;

        /// <summary>
        /// Creates a new instance of <see cref="LavaPayoffWalletCheckRequest"/> with Lava Wallet as the withdrawal destination.
        /// </summary>
        /// <param name="shopId">The project identifier.</param>
        /// <param name="walletId">The Lava Wallet ID.</param>
        /// <returns>A new instance of <see cref="LavaPayoffWalletCheckRequest"/> initialized with the provided parameters.</returns>
        /// <exception cref="ArgumentException">Thrown when the input <paramref name="walletId"/> does not match the R00000000 pattern.</exception>
        public static LavaPayoffWalletCheckRequest LavaWallet(string shopId, string walletId) => R.LavaWalletRegex().IsMatch(walletId)
            ? new LavaPayoffWalletCheckRequest(shopId, LavaPayoffService.lava_payoff, walletId)
            : throw new ArgumentException($"Input {nameof(walletId)} does not match R00000000 pattern", nameof(walletId));

        /// <summary>
        /// Creates a new instance of <see cref="LavaPayoffWalletCheckRequest"/> with a bank card as the withdrawal destination.
        /// </summary>
        /// <param name="shopId">The project identifier.</param>
        /// <param name="bankCard">The bank card number.</param>
        /// <returns>A new instance of <see cref="LavaPayoffWalletCheckRequest"/> initialized with the provided parameters.</returns>
        /// <exception cref="ArgumentException">Thrown when the input <paramref name="bankCard"/> does not match the 0000000000000000 pattern.</exception>
        public static LavaPayoffWalletCheckRequest BankCard(string shopId, string bankCard) => R.BankCardRegex().IsMatch(R.NoSpaceRegex().Replace(bankCard, ""))
            ? new LavaPayoffWalletCheckRequest(shopId, LavaPayoffService.card_payoff, bankCard)
            : throw new ArgumentException($"Input {bankCard} does not match 0000000000000000 pattern", nameof(bankCard));
    }
}