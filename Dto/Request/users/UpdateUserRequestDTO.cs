using System.Text.Json.Serialization;
using WebBuySource.Models.Enums;

namespace WebBuySource.Dto.Request.users
{
    public class UpdateUserRequestDTO
    {
        public required int Id { get; set; } 
        public string? Username { get; set; }
        public string? Fullname { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public Gender? Gender { get; set; }


        // Avatar
        /// <summary>
        /// File to upload as avatar. Leave empty if not changing.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IFormFile AvatarFile { get; set; }
        /// <summary>
        /// Avatar URL (hidden in Swagger)
        /// </summary>
        [JsonIgnore] 
        public string? Avatar { get; set; }
    }
}
