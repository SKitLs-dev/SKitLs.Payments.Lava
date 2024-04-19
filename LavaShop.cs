using Newtonsoft.Json;
using RestSharp;
using SKitLs.Payments.Lava.Common;
using SKitLs.Payments.Lava.Invoice;
using SKitLs.Payments.Lava.Payoff;
using SKitLs.Payments.Lava.Shop;
using System.Security.Cryptography;
using System.Text;

namespace SKitLs.Payments.Lava
{
    /// <summary>
    /// Represents a client for interacting with Lava payment service.
    /// </summary>
    /// <param name="shopId">The shop ID.</param>
    /// <param name="secret">The secret key for generating signatures.</param>
    public class LavaShop(string shopId, string secret)
    {
        /// <summary>
        /// Gets or sets the shop ID used for authentication and identification.
        /// </summary>
        private string ShopId { get; set; } = shopId;

        /// <summary>
        /// Gets or sets the secret key used for authentication and signature generation.
        /// </summary>
        private string Secret { get; set; } = secret;

        /// <summary>
        /// Gets or sets the additional secret key used for signature verification in hooks.
        /// </summary>
        private string? AdditionalSecret { get; set; } = null;

        /// <summary>
        /// Sets an additional secret key for WebHooks signature validation.
        /// </summary>
        /// <param name="secret">The additional secret key.</param>
        /// <returns>The current instance of <see cref="LavaShop"/>.</returns>
        public LavaShop SetAdditionalSecret(string secret)
        {
            AdditionalSecret = secret;
            return this;
        }

        /// <summary>
        /// Gets or sets the base URL for the Lava API.
        /// </summary>
        /// <remarks>
        /// This URL is used as the base for all Lava API endpoints.
        /// </remarks>
        public string BaseApiUrl { get; set; } = "https://api.lava.ru/business";

        /// <summary>
        /// Generates a signature for the given JSON body using the specified secret key.
        /// </summary>
        /// <param name="jsonBody">The JSON body for which to generate the signature.</param>
        /// <param name="secret">The secret key used for generating the signature.</param>
        /// <returns>The generated signature.</returns>
        private static string GenerateSignature(string jsonBody, string secret)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(secret);
            byte[] dataBytes = Encoding.UTF8.GetBytes(jsonBody);

            using var hmacKey = new HMACSHA256(keyBytes);
            byte[] hashBytes = hmacKey.ComputeHash(dataBytes);
            string signature = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            return signature;
        }

        /// <summary>
        /// Makes an asynchronous <see cref="Method.Post"/> request to the specified <paramref name="url"/> with the provided <paramref name="requestBody"/>, 
        /// and deserializes the response content to the specified type.
        /// </summary>
        /// <typeparam name="T">The type of object to deserialize the response content to.</typeparam>
        /// <param name="url">The URL to make the <see cref="Method.Post"/> request to.</param>
        /// <param name="requestBody">The request body to send with the <see cref="Method.Post"/> request.</param>
        /// <param name="cts">A cancellation token that can be used to cancel the asynchronous operation.</param>
        /// <returns>The deserialized response content of type <typeparamref name="T"/>.</returns>
        /// <exception cref="JsonSerializationException">Thrown when there is an error during JSON deserialization of the response content.</exception>
        /// <exception cref="HttpRequestException">Thrown when there is an error during the HTTP request, such as network issues or server errors.</exception>
        private async Task<T> LavaPostAsync<T>(string url, object requestBody, CancellationToken cts = default)
        {
            var jsonRequest = JsonConvert.SerializeObject(requestBody);

            var client = new RestClient(url);
            var request = new RestRequest
            {
                Method = Method.Post
            };
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Signature", GenerateSignature(jsonRequest, Secret));
            request.AddBody(jsonRequest);

            var response = await client.ExecuteAsync(request, cts);

            if (!string.IsNullOrEmpty(response.Content))
            {
                return JsonConvert.DeserializeObject<T>(response.Content) ?? throw new JsonSerializationException($"Was not able to deserialize {nameof(T)} ({url})");
            }
            else
            {
                throw new HttpRequestException($"Was not able to access {url}.");
            }
        }

        #region Shop

        /// <summary>
        /// Gets the URL for the shop-related API endpoints.
        /// </summary>
        /// <remarks>
        /// This URL is used as the base for shop-related API endpoints.
        /// </remarks>
        public string ShopUrl => BaseApiUrl + "/shop";

        /// <summary>
        /// <b><see href="https://dev.lava.ru/shop-balance">[API]</see></b> Gets the URL for retrieving the balance of the shop.
        /// </summary>
        public string BalanceUrl => ShopUrl + "/get-balance";

        /// <summary>
        /// <b><see href="https://dev.lava.ru/shop-balance">[API]</see></b> Asynchronously retrieves the balance of the shop.
        /// </summary>
        /// <returns>The balance of the shop as a <see cref="LavaShopBalanceResponse"/> object.</returns>
        /// <exception cref="JsonSerializationException">Thrown when there is an error during JSON deserialization of the response content.</exception>
        /// <exception cref="HttpRequestException">Thrown when there is an error during the HTTP request, such as network issues or server errors.</exception>
        public async Task<LavaShopBalanceResponse> GetBalanceAsync(CancellationToken cts = default) => await LavaPostAsync<LavaShopBalanceResponse>(BalanceUrl, new LavaShopIdRequest(ShopId), cts);
        #endregion

        #region Invoice

        /// <summary>
        /// Gets the URL for the invoice-related API endpoints.
        /// </summary>
        /// <remarks>
        /// This URL is used as the base for invoice-related API endpoints.
        /// </remarks>
        public string InvoiceUrl => BaseApiUrl + "/invoice";

        /// <summary>
        /// <b><see href="https://dev.lava.ru/api-invoice-create">[API]</see></b> Gets the URL for creating invoices.
        /// </summary>
        public string InvoiceCreateUrl => InvoiceUrl + "/create";

        /// <summary>
        /// <b><see href="https://dev.lava.ru/api-invoice-status">[API]</see></b> Gets the URL for retrieving the status of invoices.
        /// </summary>
        public string InvoiceStatusUrl => InvoiceUrl + "/status";

        /// <summary>
        /// <b><see href="https://dev.lava.ru/api-invoice-services">[API]</see></b> Gets the URL for retrieving available tariffs for invoices.
        /// </summary>
        public string InvoiceTariffsUrl => InvoiceUrl + "/get-available-tariffs";

        /// <summary>
        /// <b><see href="https://dev.lava.ru/api-invoice-create">[API]</see></b> Asynchronously creates an invoice for payment with the specified order ID and sum.
        /// </summary>
        /// <param name="orderId">The unique identifier of the transaction in the merchant's system.</param>
        /// <param name="sum">The amount of the invoice.</param>
        /// <param name="cts">A cancellation token that can be used to cancel the asynchronous operation.</param>
        /// <returns>The created invoice response as a <see cref="LavaInvoiceCreateResponse"/> object.</returns>
        /// <exception cref="JsonSerializationException">Thrown when there is an error during JSON deserialization of the response content.</exception>
        /// <exception cref="HttpRequestException">Thrown when there is an error during the HTTP request, such as network issues or server errors.</exception>
        public async Task<LavaInvoiceCreateResponse> CreateInvoiceAsync(string orderId, double sum, CancellationToken cts = default) => await CreateInvoiceAsync(new LavaInvoiceCreateRequest(sum, orderId, ShopId), cts);

        /// <summary>
        /// <b><see href="https://dev.lava.ru/api-invoice-create">[API]</see></b> Asynchronously creates an invoice for payment with the specified request body.
        /// </summary>
        /// <param name="requestBody">The request body containing the details of the invoice to create.</param>
        /// <param name="cts">A cancellation token that can be used to cancel the asynchronous operation.</param>
        /// <returns>The created invoice response as a <see cref="LavaInvoiceCreateResponse"/> object.</returns>
        /// <exception cref="JsonSerializationException">Thrown when there is an error during JSON deserialization of the response content.</exception>
        /// <exception cref="HttpRequestException">Thrown when there is an error during the HTTP request, such as network issues or server errors.</exception>
        public async Task<LavaInvoiceCreateResponse> CreateInvoiceAsync(LavaInvoiceCreateRequest requestBody, CancellationToken cts = default) => await LavaPostAsync<LavaInvoiceCreateResponse>(InvoiceCreateUrl, requestBody, cts);

        /// <summary>
        /// <b><see href="https://dev.lava.ru/api-invoice-status">[API]</see></b> Asynchronously retrieves the status of an invoice by the specified order ID.
        /// </summary>
        /// <param name="orderId">The unique identifier of the transaction in the merchant's system.</param>
        /// <param name="cts">A cancellation token that can be used to cancel the asynchronous operation.</param>
        /// <returns>The invoice status response as a <see cref="LavaInvoiceStatusResponse"/> object.</returns>
        /// <exception cref="JsonSerializationException">Thrown when there is an error during JSON deserialization of the response content.</exception>
        /// <exception cref="HttpRequestException">Thrown when there is an error during the HTTP request, such as network issues or server errors.</exception>
        public async Task<LavaInvoiceStatusResponse> GetInvoiceStatusByOrderIdAsync(string orderId, CancellationToken cts = default) => await GetInvoiceStatusAsync(LavaInvoiceStatusRequest.FromOrderId(ShopId, orderId), cts);

        /// <summary>
        /// <b><see href="https://dev.lava.ru/api-invoice-status">[API]</see></b> Asynchronously retrieves the status of an invoice by the specified invoice ID.
        /// </summary>
        /// <param name="invoiceId">The unique identifier of the invoice.</param>
        /// <param name="cts">A cancellation token that can be used to cancel the asynchronous operation.</param>
        /// <returns>The invoice status response as a <see cref="LavaInvoiceStatusResponse"/> object.</returns>
        /// <exception cref="JsonSerializationException">Thrown when there is an error during JSON deserialization of the response content.</exception>
        /// <exception cref="HttpRequestException">Thrown when there is an error during the HTTP request, such as network issues or server errors.</exception>
        public async Task<LavaInvoiceStatusResponse> GetInvoiceStatusByInvoiceIdAsync(string invoiceId, CancellationToken cts = default) => await GetInvoiceStatusAsync(LavaInvoiceStatusRequest.FromInvoiceId(ShopId, invoiceId), cts);

        /// <summary>
        /// <b><see href="https://dev.lava.ru/api-invoice-status">[API]</see></b> Asynchronously retrieves the status of an invoice based on the specified status request.
        /// </summary>
        /// <param name="statusRequest">The status request containing the details of the invoice to query.</param>
        /// <param name="cts">A cancellation token that can be used to cancel the asynchronous operation.</param>
        /// <returns>The invoice status response as a <see cref="LavaInvoiceStatusResponse"/> object.</returns>
        /// <exception cref="JsonSerializationException">Thrown when there is an error during JSON deserialization of the response content.</exception>
        /// <exception cref="HttpRequestException">Thrown when there is an error during the HTTP request, such as network issues or server errors.</exception>
        public async Task<LavaInvoiceStatusResponse> GetInvoiceStatusAsync(LavaInvoiceStatusRequest statusRequest, CancellationToken cts = default) => await LavaPostAsync<LavaInvoiceStatusResponse>(InvoiceStatusUrl, statusRequest, cts);

        /// <summary>
        /// <b><see href="https://dev.lava.ru/api-invoice-services">[API]</see></b> Asynchronously retrieves the available tariffs for invoices.
        /// </summary>
        /// <param name="cts">A cancellation token that can be used to cancel the asynchronous operation.</param>
        /// <returns>The available tariffs response as a <see cref="LavaInvoiceTariffsResponse"/> object.</returns>
        /// <exception cref="JsonSerializationException">Thrown when there is an error during JSON deserialization of the response content.</exception>
        /// <exception cref="HttpRequestException">Thrown when there is an error during the HTTP request, such as network issues or server errors.</exception>
        public async Task<LavaInvoiceTariffsResponse> GetInvoiceTariffsAsync(CancellationToken cts = default) => await LavaPostAsync<LavaInvoiceTariffsResponse>(InvoiceTariffsUrl, new LavaShopIdRequest(ShopId), cts);

        /// <summary>
        /// Checks if the provided signature matches the provided signature for the given JSON body using <see cref="AdditionalSecret"/>.
        /// </summary>
        /// <param name="jsonBody">The JSON body of the request.</param>
        /// <param name="signature">The signature to check against.</param>
        /// <returns><see langword="true"/> if the signatures match; otherwise, <see langword="false"/>.</returns>
        public bool CheckSignature(string jsonBody, string signature)
        {
            if (AdditionalSecret is null)
                throw new NullReferenceException($"{nameof(AdditionalSecret)} is not provided.");
            var gen = GenerateSignature(jsonBody, AdditionalSecret);
            return gen.Equals(signature);
        }
        #endregion

        #region Payoff

        /// <summary>
        /// Gets the URL for the payoff-related API endpoints.
        /// </summary>
        /// <remarks>
        /// This URL is used as the base for payoff-related API endpoints.
        /// </remarks>
        public string PayoffUrl => BaseApiUrl + "/payoff";

        /// <summary>
        /// <b><see href="https://dev.lava.ru/business-payoff-create">[API]</see></b> Gets the URL for creating payoffs.
        /// </summary>
        public string PayoffCreateUrl => PayoffUrl + "/create";

        /// <summary>
        /// <b><see href="https://dev.lava.ru/business-payoff-info">[API]</see></b> Gets the URL for retrieving the status of payoff.
        /// </summary>
        public string PayoffStatusUrl => PayoffUrl + "/info";

        /// <summary>
        /// <b><see href="https://dev.lava.ru/business-payoff-tariffs">[API]</see></b> Gets the URL for retrieving available tariffs for payoffs.
        /// </summary>
        public string PayoffTariffsUrl => PayoffUrl + "/get-tariffs";

        /// <summary>
        /// <b><see href="https://dev.lava.ru/business-payoff-wallet-check">[API]</see></b> Gets the URL for checking payoff wallet.
        /// </summary>
        public string PayoffWalletCheckUrl => PayoffUrl + "/get-tariffs";

        /// <summary>
        /// <b><see href="https://dev.lava.ru/business-payoff-create">[API]</see></b> Creates a payoff to a Lava wallet asynchronously.
        /// </summary>
        /// <param name="amount">The amount to be paid off.</param>
        /// <param name="orderId">The unique identifier for the order.</param>
        /// <param name="walletId">The identifier of the Lava wallet.</param>
        /// <param name="cts">A cancellation token that can be used to cancel the asynchronous operation.</param>
        /// <returns>The created payoff response as a <see cref="LavaPayoffCreateResponse"/> object.</returns>
        /// <exception cref="JsonSerializationException">Thrown when there is an error during JSON deserialization of the response content.</exception>
        /// <exception cref="HttpRequestException">Thrown when there is an error during the HTTP request, such as network issues or server errors.</exception>
        public async Task<LavaPayoffCreateResponse> CreatePayoffToLavaAsync(double amount, string orderId, string walletId, CancellationToken cts = default) => await CreatePayoffAsync(LavaPayoffCreateRequest.ToLavaWallet(amount, orderId, ShopId, walletId), cts);

        /// <summary>
        /// <b><see href="https://dev.lava.ru/business-payoff-create">[API]</see></b> Creates a payoff to a bank card asynchronously.
        /// </summary>
        /// <param name="amount">The amount to be paid off.</param>
        /// <param name="orderId">The unique identifier for the order.</param>
        /// <param name="bankCard">The bank card number to which the payoff is made.</param>
        /// <param name="cts">A cancellation token that can be used to cancel the asynchronous operation.</param>
        /// <returns>The created payoff response as a <see cref="LavaPayoffCreateResponse"/> object.</returns>
        /// <exception cref="JsonSerializationException">Thrown when there is an error during JSON deserialization of the response content.</exception>
        /// <exception cref="HttpRequestException">Thrown when there is an error during the HTTP request, such as network issues or server errors.</exception>
        public async Task<LavaPayoffCreateResponse> CreatePayoffToCardAsync(double amount, string orderId, string bankCard, CancellationToken cts = default) => await CreatePayoffAsync(LavaPayoffCreateRequest.ToBankCard(amount, orderId, ShopId, bankCard), cts);

        /// <summary>
        /// <b><see href="https://dev.lava.ru/business-payoff-create">[API]</see></b> Creates a payoff asynchronously based on the provided request.
        /// </summary>
        /// <param name="request">The request object containing payoff details.</param>
        /// <param name="cts">A cancellation token that can be used to cancel the asynchronous operation.</param>
        /// <returns>The created payoff response as a <see cref="LavaPayoffCreateResponse"/> object.</returns>
        /// <exception cref="JsonSerializationException">Thrown when there is an error during JSON deserialization of the response content.</exception>
        /// <exception cref="HttpRequestException">Thrown when there is an error during the HTTP request, such as network issues or server errors.</exception>
        public async Task<LavaPayoffCreateResponse> CreatePayoffAsync(LavaPayoffCreateRequest request, CancellationToken cts = default) => await LavaPostAsync<LavaPayoffCreateResponse>(PayoffCreateUrl, request, cts);

        /// <summary>
        /// <b><see href="https://dev.lava.ru/api-invoice-status">[API]</see></b> Retrieves the payoff status by order ID asynchronously.
        /// </summary>
        /// <param name="orderId">The unique identifier for the order.</param>
        /// <param name="cts">A cancellation token that can be used to cancel the asynchronous operation.</param>
        /// <returns>The payoff status response as a <see cref="LavaPayoffStatusResponse"/> object.</returns>
        /// <exception cref="JsonSerializationException">Thrown when there is an error during JSON deserialization of the response content.</exception>
        /// <exception cref="HttpRequestException">Thrown when there is an error during the HTTP request, such as network issues or server errors.</exception>
        public async Task<LavaPayoffStatusResponse> GetPayoffStatusByOrderIdAsync(string orderId, CancellationToken cts = default) => await GetPayoffStatusAsync(LavaPayoffStatusRequest.FromOrderId(ShopId, orderId), cts);

        /// <summary>
        /// <b><see href="https://dev.lava.ru/api-invoice-status">[API]</see></b> Retrieves the payoff status by payoff ID asynchronously.
        /// </summary>
        /// <param name="payoffId">The unique identifier for the payoff.</param>
        /// <param name="cts">A cancellation token that can be used to cancel the asynchronous operation.</param>
        /// <returns>The payoff status response as a <see cref="LavaPayoffStatusResponse"/> object.</returns>
        /// <exception cref="JsonSerializationException">Thrown when there is an error during JSON deserialization of the response content.</exception>
        /// <exception cref="HttpRequestException">Thrown when there is an error during the HTTP request, such as network issues or server errors.</exception>
        public async Task<LavaPayoffStatusResponse> GetPayoffStatusByPayoffIdAsync(string payoffId, CancellationToken cts = default) => await GetPayoffStatusAsync(LavaPayoffStatusRequest.FromPayoffId(ShopId, payoffId), cts);

        /// <summary>
        /// <b><see href="https://dev.lava.ru/api-invoice-status">[API]</see></b> Retrieves the payoff status asynchronously based on the provided request.
        /// </summary>
        /// <param name="request">The request object containing payoff details.</param>
        /// <param name="cts">A cancellation token that can be used to cancel the asynchronous operation.</param>
        /// <returns>The payoff status response as a <see cref="LavaPayoffStatusResponse"/> object.</returns>
        /// <exception cref="JsonSerializationException">Thrown when there is an error during JSON deserialization of the response content.</exception>
        /// <exception cref="HttpRequestException">Thrown when there is an error during the HTTP request, such as network issues or server errors.</exception>
        public async Task<LavaPayoffStatusResponse> GetPayoffStatusAsync(LavaPayoffStatusRequest request, CancellationToken cts = default) => await LavaPostAsync<LavaPayoffStatusResponse>(PayoffStatusUrl, request, cts);

        /// <summary>
        /// <b><see href="https://dev.lava.ru/business-payoff-tariffs">[API]</see></b> Retrieves the available payoff tariffs asynchronously.
        /// </summary>
        /// <param name="cts">A cancellation token that can be used to cancel the asynchronous operation.</param>
        /// <returns>The payoff tariffs response as a <see cref="LavaPayoffTariffsResponse"/> object.</returns>
        /// <exception cref="JsonSerializationException">Thrown when there is an error during JSON deserialization of the response content.</exception>
        /// <exception cref="HttpRequestException">Thrown when there is an error during the HTTP request, such as network issues or server errors.</exception>
        public async Task<LavaPayoffTariffsResponse> GetPayoffTariffsAsync(CancellationToken cts = default) => await LavaPostAsync<LavaPayoffTariffsResponse>(PayoffTariffsUrl, new LavaShopIdRequest(ShopId));

        /// <summary>
        /// <b><see href="https://dev.lava.ru/business-payoff-wallet-check">[API]</see></b> Checks the availability of payoff to a Lava wallet asynchronously.
        /// </summary>
        /// <param name="walletId">The identifier of the Lava wallet.</param>
        /// <param name="cts">A cancellation token that can be used to cancel the asynchronous operation.</param>
        /// <returns>The payoff wallet check response as a <see cref="LavaPayoffWalletCheckResponse"/> object.</returns>
        /// <exception cref="JsonSerializationException">Thrown when there is an error during JSON deserialization of the response content.</exception>
        /// <exception cref="HttpRequestException">Thrown when there is an error during the HTTP request, such as network issues or server errors.</exception>
        public async Task<LavaPayoffWalletCheckResponse> CheckPayoffLavaWalletAsync(string walletId, CancellationToken cts = default) => await CheckPayoffWalletAsync(LavaPayoffWalletCheckRequest.LavaWallet(ShopId, walletId), cts);

        /// <summary>
        /// <b><see href="https://dev.lava.ru/business-payoff-wallet-check">[API]</see></b> Checks the availability of payoff to a bank wallet asynchronously.
        /// </summary>
        /// <param name="bankCard">The bank card number to which the payoff is made.</param>
        /// <param name="cts">A cancellation token that can be used to cancel the asynchronous operation.</param>
        /// <returns>The payoff wallet check response as a <see cref="LavaPayoffWalletCheckResponse"/> object.</returns>
        /// <exception cref="JsonSerializationException">Thrown when there is an error during JSON deserialization of the response content.</exception>
        /// <exception cref="HttpRequestException">Thrown when there is an error during the HTTP request, such as network issues or server errors.</exception>
        public async Task<LavaPayoffWalletCheckResponse> CheckPayoffBankWalletAsync(string bankCard, CancellationToken cts = default) => await CheckPayoffWalletAsync(LavaPayoffWalletCheckRequest.BankCard(ShopId, bankCard), cts);

        /// <summary>
        /// <b><see href="https://dev.lava.ru/business-payoff-wallet-check">[API]</see></b> Checks the availability of payoff asynchronously based on the provided request.
        /// </summary>
        /// <param name="request">The request object containing wallet check details.</param>
        /// <param name="cts">A cancellation token that can be used to cancel the asynchronous operation.</param>
        /// <returns>The payoff wallet check response as a <see cref="LavaPayoffWalletCheckResponse"/> object.</returns>
        /// <exception cref="JsonSerializationException">Thrown when there is an error during JSON deserialization of the response content.</exception>
        /// <exception cref="HttpRequestException">Thrown when there is an error during the HTTP request, such as network issues or server errors.</exception>
        public async Task<LavaPayoffWalletCheckResponse> CheckPayoffWalletAsync(LavaPayoffWalletCheckRequest request, CancellationToken cts = default) => await LavaPostAsync<LavaPayoffWalletCheckResponse>(PayoffWalletCheckUrl, request, cts);
        #endregion
    }
}