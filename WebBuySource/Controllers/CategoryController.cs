
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebBuySource.Dto.Request.Category;
using WebBuySource.Dto.Response;
using WebBuySource.Interfaces;

namespace WebBuySource.Controllers
{
    [ApiController]
    [Route("api/v1/category")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        #region Constructor
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        #endregion

        /// <summary>
        /// Retrieve all categories (with pagination).
        /// </summary>
        /// <remarks>Only accessible by users with the Admin role.</remarks>
        /// /// <returns>The requested attchment of current user.</returns>
        /// <response code="200">The Product  was successfully retrieved.</response>
        /// <response code="400">The Product  is invalid.</response>
        /// <response code="401">The Product  user not have permission to access this function.</response>
        [HttpGet]
        public async Task<BaseAPIResponse> GetAllCategory([FromQuery] CategoryRequestDTO request)
        {
            return await _categoryService.GetAllCategory(request);
        }

        /// <summary>
        /// Create a new category.
        /// </summary>
        /// <remarks>Requires Admin role. Returns the created category information.</remarks>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        /// <summary>
        /// Retrieve all categories (with pagination).
        /// </summary>
        /// <remarks>Only accessible by users with the Admin role.</remarks>
        /// /// <returns>The requested attchment of current user.</returns>
        /// <response code="200">The Product  was successfully retrieved.</response>
        /// <response code="400">The Product  is invalid.</response>
        /// <response code="401">The Product  user not have permission to access this function.</response>
        public async Task<BaseAPIResponse> AddCategory([FromBody] CategoryRequestDTO input)
        {
            return await _categoryService.AddCategory(input);
        }





        /// <summary>
        /// Update an existing category.
        /// </summary>
        /// <remarks>Only accessible by users with the Admin role.</remarks>
        /// /// <returns>The requested attchment of current user.</returns>
        /// <response code="200">The Product  was successfully retrieved.</response>
        /// <response code="400">The Product  is invalid.</response>
        /// <response code="401">The Product  user not have permission to access this function.</response>
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<BaseAPIResponse> UpdateCategory([FromBody] CategoryRequestDTO request)
        {
            return await _categoryService.UpdateCategory(request);
        }

        /// <summary>
        /// Delete a category by its ID.
        /// </summary>
        /// <remarks>Requires Admin role. Permanently removes the category.</remarks>
        /// <remarks>Only accessible by users with the Admin role.</remarks>
        /// /// <returns>The requested attchment of current user.</returns>
        /// <response code="200">The Product  was successfully retrieved.</response>
        /// <response code="400">The Product  is invalid.</response>
        /// <response code="401">The Product  user not have permission to access this function.</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<BaseAPIResponse> DeleteCategory(int id)
        {
            return await _categoryService.DeleteCategory(id);
        }
    }
}

