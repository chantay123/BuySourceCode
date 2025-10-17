
//using Microsoft.AspNetCore.Mvc;
//using WebBanNongSan.Dto.Response;
//using WebBanNongSan.Dto.Request.ProductRequest;
//using WebBuySource.Interfaces;
//using WebBuySource.Dto.Request.ProductRequest;

//namespace WebBuySource.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class ProductController : Controller
//    {
//        #region Reference services
//        /// <summary>
//        /// The attchament detection service
//        /// </summary>
//        private readonly IProductService _productService;
//        #endregion

//        #region Contructor
//        public ProductController(IProductService productService)
//        {
//            // The product  service
//            _productService = productService;
//        }
//        #endregion

//        #region Get Product.
//        /// <returns></returns>
//        /// <summary>
//        /// Get Product .
//        /// </summary>
//        /// <param name="Get ">Condition to get all notify.</param>
//        /// <returns>The requested attchment of current user.</returns>
//        /// <response code="200">The Product  was successfully retrieved.</response>
//        /// <response code="400">The Product  is invalid.</response>
//        /// <response code="401">The Product   user not have permission to access this function.</response>
//        [HttpGet]
//        public async Task<BaseAPIResponse> GetAllProduct([FromQuery] ProductRequestDTO request)
//        {
//            // Call get all Product.
//            return await _productService.GetAllProduct(request);
//        }
//        #endregion

//        #region Get Product.
//        /// <returns></returns>
//        /// <summary>
//        /// Get Product .
//        /// </summary>
//        /// <param name="Get ">Condition to get all notify.</param>
//        /// <returns>The requested attchment of current user.</returns>
//        /// <response code="200">The Product  was successfully retrieved.</response>
//        /// <response code="400">The Product  is invalid.</response>
//        /// <response code="401">The Product   user not have permission to access this function.</response>
//        [HttpPost]
//        public async Task<BaseAPIResponse> AddProduct([FromBody] AddProductRequestDTO input)
//        {
//            // Call get all Product.
//            return await _productService.AddProduct(input);
//        }
//        #endregion

//        /// <returns></returns>
//        /// <summary>
//        ///  Put Product .//        /// </summary>
//        /// <param name="Get ">Condition to get all notify.</param>
//        /// <returns>The requested attchment of current user.</returns>
//        /// <response code="200">The Product  was successfully retrieved.</response>
//        /// <response code="400">The Product  is invalid.</response>
//        /// <response code="401">The Product   user not have permission to access this function.</response>
//        [HttpPut]
//        public async Task<BaseAPIResponse> UpdateProduct([FromBody] ProductRequestDTO request)
//        {
//            return await _productService.UpdateProduct(request);
//        }
//    }
//}

