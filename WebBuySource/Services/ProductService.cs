//using WebBanNongSan.Dto.Request.ProductRequest;
//using WebBanNongSan.Dto.Response;
//using WebBanNongSan.Dto.Response.ProductResponse;
//using WebBuySource.Dto.Request.ProductRequest;
//using WebBuySource.Interfaces;
//using WebBuySource.Models;
//using WebBuySource.Services;
//using WebBuySource.Utilities;

//namespace WebBanNongSan.Services
//{
//    public class ProductService : BaseService, IProductService
//    {
//        #region Reference repository
//        /// <summary>
//        /// Gets the product repository.
//        /// </summary>
//        //private IRepository<ProductModel> ProductRepository => UnitOfWork.ProductRepository;

//        //private IRepository<CategoryModel> CategoryRepository => UnitOfWork.CategoryRepository;

//        #endregion

//        #region Contructor
//        public ProductService(IUnitOfWork unitOfWork) : base(unitOfWork)
//        {

//        }
//        #endregion
//        public async Task<BaseAPIResponse> GetAllProduct(ProductRequestDTO request)
//        {
//            var result = ProductRepository.GetAllAsNoTracking().
//                Select(x => new ProductResponse
//                {
//                    Id = x.ProductId,
//                    Name = x.ProductName,
//                    Description = x.Description,
//                    Price = x.Price,
//                }).ToList();

//            return BaseApiResponse.OK(result);
//        }

//        /// <summary>
//        /// Get product by category
//        /// </summary>
//        /// <param name="request"></param>
//        /// <returns></returns>
//        //public async Task<BaseAPIResponse> GetAllProduct(ProductRequestDTO request)
//        //{
//        //    var result = CategoryRepository.GetAll().Join(ProductRepository.GetAll(), x => x.CategoryId, y => y.CategoryId, (x, y) => new
//        //    {
//        //        x.CategoryId,
//        //        x.CategoryName,
//        //        y.ProductName,

//        //    }).ToList();

//        //    return BaseApiResponse.OK(result);
//        //}

//        /// <summary>
//        /// Add Product
//        /// </summary>
//        /// <param name="input"></param>
//        /// <returns></returns>
//        public async Task<BaseAPIResponse> AddProduct(AddProductRequestDTO input)
//        {
//            var product = new ProductModel
//            {
//                ProductName = input.Name,
//                Description = input.Description,
//                Price = input.Price,
//                CategoryId = input.CategoryId,
//            };
//            await ProductRepository.AddAsync(product);

//            await ProductRepository.SaveChangesAsync();

//            return BaseApiResponse.OK();
//        }

//        /// <summary>
//        /// update Product
//        /// </summary>
//        /// <param name="request"></param>
//        /// <returns></returns>
//        /// <exception cref="NotImplementedException"></exception>
//        public Task<BaseAPIResponse> UpdateProduct(ProductRequestDTO request)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
