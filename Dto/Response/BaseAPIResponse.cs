using System.Text.Json.Serialization;

namespace WebBuySource.Dto.Response
{
    /// <summary>
    /// Base API response class used across the system.
    /// </summary>
    public class BaseAPIResponse
    {
        /// <summary>
        /// Process status: true = success, false = error
        /// </summary>
        [JsonPropertyName("success")]
        public bool? Success { get; set; }

        /// <summary>
        /// HTTP or business status code
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("statusCode")]
        public int? StatusCode { get; set; }

        /// <summary>
        /// Short message or code that identifies the message type
        /// </summary>
        ///
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("msgCode")]
        public string MessageCode { get; set; }

        /// <summary>
        /// Detailed message about the response (human-readable)
        /// </summary>
        /// 
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("msgDetail")]
        public string MessageDetail { get; set; }

        /// <summary>
        /// Total number of records (optional)
        /// </summary>
        /// 
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("total")]
        public int? Total { get; set; }

        /// <summary>
        /// Page size used in pagination (optional)
        /// </summary>
        /// 
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("pageSize")]
        public int? PageSize { get; set; }

        /// <summary>
        /// Generic response data — now replaces 'Items'
        /// </summary>
        /// 
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("data")]
        public object Items { get; set; }

        /// <summary>
        /// Total number of unread items (optional)
        /// </summary>
        /// 
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("totalUnRead")]
        public int? TotalUnRead { get; set; }

        /// <summary>
        /// Indicates whether the response contains warnings
        /// </summary>
        /// 
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("isWarning")]
        public bool? IsWarning { get; set; }

        /// <summary>
        /// Timestamp of the response (optional)
        /// </summary>
        [JsonPropertyName("timestamp")]
        public DateTime? Timestamp { get; set; } = DateTime.UtcNow;
    }
}
