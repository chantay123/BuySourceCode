using System.Text.Json.Serialization;

namespace WebBuySource.Dto.Response.CategoryResponse
{
    public class CategoryResponse
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

        [JsonPropertyName("category_Dis")]
        public string Description { get; set; }
    }
}
