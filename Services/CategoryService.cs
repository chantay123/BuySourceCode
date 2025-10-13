
using WebBanNongSan.Dto.Request;
using WebBanNongSan.Dto.Response;
using WebBanNongSan.Dto.Response.CategoryResponse;
using WebBuySource.Models;
using WebBuySource.Interfaces;
using WebBuySource.Utilities;

namespace WebBuySource.Services
{
    public class CategoryService : BaseService, ICategoryService
    {

        #region Reference repository
        /// <summary>
        /// Gets the product repository.
        /// </summary>
        private IRepository<Category> CategoyRepository => UnitOfWork.CategoryRepository;

        #endregion

        public CategoryService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task<BaseAPIResponse> GetAllCategory(CategoryRequestDTO request)
        {
            var result = CategoyRepository.GetAllAsNoTracking().Select(x => new CategoryResponse
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,    
            });


            return BaseApiResponse.OK(result);
        }

        //public async Task<BaseAPIResponse> AddCategory(CategoryRequestDTO input)
        //{
        //    var category = new CategoryModel
        //    {
        //        CategoryId = input.Id,
        //        CategoryName = input.Name,
        //    };

        //    ///Add new row category
        //    await CategoyRepository.AddAsync(category).ConfigureAwait(false); ;
        //    //Save changes 
        //    await CategoyRepository.SaveChangesAsync();
        //    return BaseApiResponse.OK();
        //}

        //public async Task<BaseAPIResponse> UpdateCategory(CategoryRequestDTO request)
        //{
        //    // Get id category
        //    var editCategory = CategoyRepository.GetById(request.Id);

        //    //check id exits
        //    if (editCategory == null)
        //    {
        //        return BaseApiResponse.NotFound();
        //    }
        //    editCategory.CategoryId = request.Id;
        //    editCategory.CategoryName = request.Name;

        //    //Update category
        //    CategoyRepository.Update(editCategory);
        //    //Save Changes database 
        //    CategoyRepository.SaveChanges();
        //    return BaseApiResponse.OK(editCategory);
        //}

        //public Task<BaseAPIResponse> DeleteCategory(Guid Id)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
