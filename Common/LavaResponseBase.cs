using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace SKitLs.Payments.Lava.Common
{
    /// <summary>
    /// Represents a base class for Lava API responses.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LavaResponseBase<T>
    {
        /// <summary>
        /// Data related to the Lava response.
        /// </summary>
        [JsonProperty("data")]
        public T? Data { get; set; }

        /// <summary>
        /// <see href="https://en.wikipedia.org/wiki/List_of_HTTP_status_codes">HTTP status code</see> of the response.
        /// </summary>
        [JsonProperty("status")]
        public int Status { get; set; }

        /// <summary>
        /// Indicates whether the status of the response is successful.
        /// </summary>
        [JsonProperty("status_check")]
        [MemberNotNullWhen(returnValue: true, nameof(Data))]
        [MemberNotNullWhen(returnValue: false, nameof(ErrorData))]
        public bool StatusCheck { get; set; }

        /// <summary>
        /// Details of any error that occurred during the request.
        /// </summary>
        [JsonProperty("error")]
        public object? ErrorData { get; set; }
    }
}