using System.Text.Json.Serialization;

namespace WebBanNongSan.Dto.Response
{
    public class BaseAPIResponse
    {
        /// <summary>
        /// Process status: true:success | false: error
        /// </summary>
        public bool? IsSuccess { get; set; }
        /// <summary>
        /// Process status code
        /// </summary>
        public int? StatusCode { get; set; }
        /// <summary>
        /// Process message code
        /// </summary>
        [JsonPropertyName("msgCode")]
        public string MessageCode { get; set; }
        /// <summary>
        /// Process message detail
        /// </summary>
        [JsonPropertyName("msgDetail")]
        public string MessageDetail { get; set; }
        /// <summary>
        /// Total records
        /// </summary>
        public int? Total { get; set; }
        /// <summary>
        /// Page size for mobile
        /// </summary>
        public int? PageSize { get; set; }
        /// <summary>
        /// Data response
        /// </summary>
        public object Items { get; set; }
        /// <summary>
        /// Gets or sets the total un read.
        /// </summary>
        /// <value>
        /// The total un read.
        /// </value>
        public int? TotalUnRead { get; set; }

        /// <summary>
        /// Is Warning messages
        /// </summary>
        public bool? IsWarning { get; set; }
    }
}
