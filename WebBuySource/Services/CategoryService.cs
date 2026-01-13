using WebBuySource.Dto.Response.CategoryResponse;
using WebBuySource.Models;
using WebBuySource.Interfaces;
using WebBuySource.Utilities;
using WebBuySource.Dto.Request.Category;
using Microsoft.EntityFrameworkCore;
using WebBuySource.Dto.Response;


namespace WebBuySource.Services
{
    public class CategoryService : BaseService, ICategoryService
    {
        #region Repository
        private IRepository<Category> CategoryRepository => UnitOfWork.CategoryRepository;
        #endregion

        public CategoryService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// Get all categories
        /// </summary>
        public async Task<BaseAPIResponse> GetAllCategory(CategoryRequestDTO request)
        {
            var items = await CategoryRepository.GetAllAsNoTracking()
            .Select(c => new CategoryResponse
            { 
                Id = c.Id,
                Name = c.Name,
                Description = c.Description ?? string.Empty,
                ImageUrl = c.ImageUrl
            })
            .ToListAsync();

            return BaseApiResponse.OK(items);

        }


        /// <summary>
        /// Add a new category
        /// </summary>
        public async Task<BaseAPIResponse> AddCategory(CategoryRequestDTO input)
        {
            if (string.IsNullOrWhiteSpace(input.Name))
                return BaseApiResponse.Error("Category name is required.");

            var newCategory = new Category
            {
                Name = input.Name,
                Description = input.Description,
                ParentId = input.ParentId,
                ImageUrl = input.ImageUrl
            };

            await CategoryRepository.AddAsync(newCategory);
            await CategoryRepository.SaveChangesAsync();

            var response = new CategoryResponse
            {
                Id = newCategory.Id,
                Name = newCategory.Name,
                Description = newCategory.Description ?? string.Empty,
                ImageUrl = newCategory.ImageUrl 
            }; 

            return BaseApiResponse.OK(response, "Category created successfully.");
        }

        /// <summary>
        /// Update category information
        /// </summary>
        public async Task<BaseAPIResponse> UpdateCategory(CategoryRequestDTO request)
        {
            var category = await CategoryRepository.GetByIdAsync(request.Id);

            if (category == null)
                return BaseApiResponse.NotFound("Category not found.");

            category.Name = request.Name ?? category.Name;
            category.Description = request.Description ?? category.Description;
            category.ParentId = request.ParentId ?? category.ParentId;

            CategoryRepository.Update(category);
            await CategoryRepository.SaveChangesAsync();

            var response = new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description ?? string.Empty
            };

            return BaseApiResponse.OK(response, "Category updated successfully.");
        }

        /// <summary>
        /// Delete a category by ID
        /// </summary>
        public async Task<BaseAPIResponse> DeleteCategory(int id)
        {
         
            var category = await CategoryRepository.GetByIdAsync(id);
            if (category == null)
                return BaseApiResponse.NotFound("Category not found.");

            CategoryRepository.Delete(category);
            await CategoryRepository.SaveChangesAsync();

            return BaseApiResponse.OK("Category deleted successfully.");
        }
    }
}
