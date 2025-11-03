namespace WebBuySource.Dto.Request.Category
{
    public class CategoryRequestDTO 
    {
        /// <summary>
        /// Category id
        /// </summary>
        public string? Id { get; set; }

            
        /// <summary>
        /// Category Name
        /// </summary>
        public string? Name { get; set; }


        /// <summary>
        ///Parent
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Description 
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// PageIndex
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// PageSize
        /// </summary>
        public int PageSize { get; set; }



    }
}
