using Microsoft.AspNetCore.Mvc;
using WebBanNongSan.Dto.Request;
using WebBanNongSan.Dto.Response;
using WebBuySource.Interfaces;

namespace WebBuySource.Controllers
{

    [ApiController]
    [Route("api/v1/category")]

    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        # region Contructor
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        #endregion
         
        /// <returns></returns>
        /// <summary>
        /// Get Product .
        /// </summary>
        /// <param name="Get ">Condition to get all notify.</param>
        /// <returns>The requested attchment of current user.</returns>
        /// <response code="200">The Product  was successfully retrieved.</response>
        /// <response code="400">The Product  is invalid.</response>
        /// <response code="401">The Product   user not have permission to access this function.</response>
        [HttpGet]
        public async Task<BaseAPIResponse> GetAllCategory([FromQuery] CategoryRequestDTO request)
        {
            // Call get all Attachment of current user.
            return await _categoryService.GetAllCategory(request);
        }

        /// <returns></returns>
        /// <summary>
        /// Get Product .
        /// </summary>
        /// <param name="Get ">Condition to get all notify.</param>
        /// <returns>The requested attchment of current user.</returns>
        /// <response code="200">The Product  was successfully retrieved.</response>
        /// <response code="400">The Product  is invalid.</response>
        /// <response code="401">The Product   user not have permission to access this function.</response>
     //   [HttpPost]
    //    public async Task<BaseAPIResponse> AddCategory([FromBody] CategoryRequestDTO input)
    //    {
    //        // Call Add Category.
    //        return await _categoryService.AddCategory(input);
    //    }

    //    /// <returns></returns>
    //    /// <summary>
    //    /// Get Product .
    //    /// </summary>
    //    /// <param name="Get ">Condition to get all notify.</param>
    //    /// <returns>The requested attchment of current user.</returns>
    //    /// <response code="200">The Product  was successfully retrieved.</response>
    //    /// <response code="400">The Product  is invalid.</response>
    //    /// <response code="401">The Product   user not have permission to access this function.</response>
    //    [HttpPut]
    //    public async Task<BaseAPIResponse> UpdateCategory([FromBody] CategoryRequestDTO request)
    //    {
    //        // Call update Category.
    //        return await _categoryService.UpdateCategory(request);
    //    }

    }
}
