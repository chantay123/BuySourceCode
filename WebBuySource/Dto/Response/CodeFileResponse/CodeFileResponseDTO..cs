namespace WebBuySource.Dto.Response.CodeFile
{
    public class CodeFileResponse
    {
        public int Id { get; set; }
        public int CodeId { get; set; }
        public string FileUrl { get; set; }
        public long? FileSize { get; set; }
        public string Version { get; set; }
        public bool IsCurrent { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
