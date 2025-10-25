using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebBuySource.Dto.Request.Category;
using WebBuySource.Dto.Response;
using WebBuySource.Interfaces;

namespace WebBuySource.Controllers
{
    [ApiController]
    [Route("api/v1/category")]
    //[Authorize(Roles = "Admin")] 
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
        [HttpGet]
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status200OK)] 
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status400BadRequest)] 
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status401Unauthorized)] 
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status403Forbidden)] 
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status500InternalServerError)] 
        public async Task<BaseAPIResponse> GetAllCategory([FromQuery] CategoryRequestDTO request)
        {
            return await _categoryService.GetAllCategory(request);
        }

        /// <summary>
        /// Create a new category.
        /// </summary>
        /// <remarks>Requires Admin role. Returns the created category information.</remarks>
        [HttpPost]
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status201Created)] 
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status400BadRequest)] 
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status401Unauthorized)] 
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status403Forbidden)] 
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status409Conflict)] 
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status500InternalServerError)] 
        public async Task<BaseAPIResponse> AddCategory([FromBody] CategoryRequestDTO input)
        {
            return await _categoryService.AddCategory(input);
        }

        /// <summary>
        /// Update an existing category.
        /// </summary>
        /// <remarks>Requires Admin role. Updates category information by ID.</remarks>
        [HttpPut]
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status200OK)] 
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status400BadRequest)] 
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status401Unauthorized)] 
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status403Forbidden)] 
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status404NotFound)] 
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status500InternalServerError)] 
        public async Task<BaseAPIResponse> UpdateCategory([FromBody] CategoryRequestDTO request)
        {
            return await _categoryService.UpdateCategory(request);
        }

        /// <summary>
        /// Delete a category by its ID.
        /// </summary>
        /// <remarks>Requires Admin role. Permanently removes the category.</remarks>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status200OK)] 
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status400BadRequest)] 
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status401Unauthorized)] 
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status403Forbidden)] 
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status404NotFound)] 
        [ProducesResponseType(typeof(BaseAPIResponse), StatusCodes.Status500InternalServerError)] 
        public async Task<BaseAPIResponse> DeleteCategory(string id)
        {
            return await _categoryService.DeleteCategory(id);
        }
    }
}
