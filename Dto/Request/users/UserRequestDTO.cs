namespace WebBuySource.Dto.Request.users
{
    public class UserRequestDTO
    {
        public string? Keyword { get; set; }    // từ khóa search
        public int Page { get; set; } = 1;      // số trang
        public int PageSize { get; set; } = 10; // số bản ghi / trang
        public string? SortBy { get; set; } = "Id";       // cột sort
        public string? SortDirection { get; set; } = "asc"; // asc / desc
        public int? RoleId { get; set; }
    }
}
