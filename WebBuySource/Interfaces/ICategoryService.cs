using WebBuySource.Dto.Request.Category;
using WebBuySource.Dto.Response;

namespace WebBuySource.Interfaces
{
    public interface ICategoryService
    {
        /// <summary>
        /// Get All Category
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseAPIResponse> GetAllCategory(CategoryRequestDTO request);
        /// <summary>
        /// Add Category
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<BaseAPIResponse> AddCategory(CategoryRequestDTO input);

        /// <summary>
        /// Update Category
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseAPIResponse> UpdateCategory(CategoryRequestDTO request);

        /// <summary>
        /// Delete category
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<BaseAPIResponse> DeleteCategory(int Id);
    }
}
