namespace WebBuySource.Dto.Request.Comment
{
    public class CommentRequestDTO
    {
        /// <summary>
        ///  code Id
        /// </summary>
        public int CodeId { get; set; }
        /// <summary>
        /// Rate
        /// </summary>
        public int Rating { get; set; } // 1–5

        /// <summary>
        /// comment text 
        /// </summary>
        public string CommentText { get; set; }

        /// <summary>
        /// parentid
        /// </summary> 
        public int? ParentId { get; set; }

        public int PageIndex { get; set; } = 1;

        public int PageSize { get; set; } = 10;
       
    }
}
