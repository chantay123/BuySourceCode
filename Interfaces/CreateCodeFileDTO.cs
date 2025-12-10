namespace WebBuySource.Interfaces
{
    public class CreateCodeFileDTO
    {
        public int CodeId { get; set; }
        public string FileUrl { get; set; } = string.Empty;
        public long? FileSize { get; set; }
        public string Version { get; set; } = "1.0.0";
        public bool IsCurrent { get; set; } = true;
    }
}