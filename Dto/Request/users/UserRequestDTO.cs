namespace WebBuySource.Dto.Request.users
{
    public class UserRequestDTO
    {
        public string? Keyword { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SortBy { get; set; } = "Id";
        public string? SortDirection { get; set; } = "asc";
        public int? RoleId { get; set; }

        //Avatar
        public IFormFile? AvatarFile { get; set; }
        public string? Avatar { get; set; }
    }
}
