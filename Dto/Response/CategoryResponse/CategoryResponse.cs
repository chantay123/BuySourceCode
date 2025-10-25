using System.Text.Json.Serialization;

namespace WebBuySource.Dto.Response.CategoryResponse
{
    public class CategoryResponse :BaseAPIResponse
    {

        /// <summary>
        /// Category id
        /// </summary>
        ///  
        [JsonPropertyName("category_id")]
        public int Id { get; set; }

        /// <summary>
        /// Category Name
        /// </summary>
        /// 
        [JsonPropertyName("category_name")]
        public string Name { get; set; }

        /// <summary>
        /// description
        /// </summary>
        [JsonPropertyName("category_dis")]
        public string Description { get; set; }

        /// <summary>
        /// ParentId
        /// </summary>
        /// 
        [JsonPropertyName("category_darentId")]
        public int? ParentId { get; set; }
    }
}
