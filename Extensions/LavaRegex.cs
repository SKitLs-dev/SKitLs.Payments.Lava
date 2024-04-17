using System.Text.RegularExpressions;

namespace SKitLs.Payments.Lava.Extensions
{
    internal static partial class R
    {
        [GeneratedRegex(@"\s")]
        public static partial Regex NoSpaceRegex();

        [GeneratedRegex(@"^R\d{8}$")]
        public static partial Regex LavaWalletRegex();

        [GeneratedRegex(@"^\d{16}$")]
        public static partial Regex BankCardRegex();
    }
}