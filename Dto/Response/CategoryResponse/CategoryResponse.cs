using System.Text.Json.Serialization;

namespace WebBuySource.Dto.Response.CategoryResponse
{
    public class CategoryResponse
    {

        /// <summary>
        /// Category id
        /// </summary>
        ///  
        [JsonPropertyName("categoryId")]
        public int? Id { get; set; }

        /// <summary>
        /// Category Name
        /// </summary>
        /// 
        [JsonPropertyName("categoryName")]
        public required string Name { get; set; }

        /// <summary>
        /// description
        /// </summary>
        [JsonPropertyName("categoryDis")]
        public string? Description { get; set; }
        /// <summary>
        /// description
        /// </summary>
        [JsonPropertyName("ImagerURL")]
        public string? ImageUrl { get; set; }

        /// <summary>
        /// ParentId
        /// </summary>
        /// 
        [JsonPropertyName("category_darentId")]
        public int? ParentId { get; set; }
    }
}
