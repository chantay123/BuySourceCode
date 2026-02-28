using Microsoft.EntityFrameworkCore;
using WebBuySource.Dto.Request.Code;
using WebBuySource.Dto.Response;
using WebBuySource.Dto.Response.Code;
using WebBuySource.Interfaces;
using WebBuySource.Models;
using WebBuySource.Utilities;

namespace WebBuySource.Services
{
    public class CodeService : BaseService, ICodeService

    {
        #region Repository
        private IRepository<Code> CodeRepository => UnitOfWork.CodeRepository;

        private IRepository<User> UserRepository => UnitOfWork.UserRepository;

        private IRepository<Category> CategoryRepository => UnitOfWork.CategoryRepository;

        private IRepository<CodeLike> CodeLikeRepository => UnitOfWork.CodeLikeRepository;

        private IRepository<ProgrammingLanguage> ProgrammingLanguageRepository => UnitOfWork.ProgrammingLanguageRepository;
        #endregion
        public CodeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

       /// <summary>
       /// getallcode
       /// </summary>
       /// <param name="request"></param>
       /// <returns></returns>
        public async Task<BaseAPIResponse> GetAllCodes(CodeRequestDTO request)
        {
            var items = await CodeRepository
                .GetAllAsNoTracking()
                .Select(x => new CodeResponse
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Price = x.Price,
                    Currency = x.Currency,
                    DemoUrl = x.DemoUrl,
                    PreviewImage = x.PreviewImage,
                    ThumbnailUrl = x.ThumbnailUrl,
                    Status = x.Status.ToString(),
                    Views = x.Views,
                    Downloads = x.Downloads,
                    AvgRating = x.AvgRating,
                    IsFeatured = x.IsFeatured,
                    LicenseType = x.LicenseType.ToString(),
                    SellerId = x.SellerId,
                    CategoryId = x.CategoryId,
                    ProgrammingLanguageId = x.ProgrammingLanguageId
                }).ToListAsync();

            return BaseApiResponse.OK(items);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BaseAPIResponse> GetCodeById(int id)
        {
            var code = await CodeRepository.GetAllAsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (code == null)
                return BaseApiResponse.NotFound("Code not found.");

            var response = new CodeResponse
            {
                Id = code.Id,
                Title = code.Title,
                Description = code.Description ?? string.Empty,
                Price = code.Price,
                Currency = code.Currency,
                DemoUrl = code.DemoUrl,
                PreviewImage = code.PreviewImage,
                ThumbnailUrl = code.ThumbnailUrl,
                Status = code.Status,
                Views = code.Views,
                Downloads = code.Downloads,
                AvgRating = code.AvgRating,
                IsFeatured = code.IsFeatured,
                LicenseType = code.LicenseType,
                SellerId = code.SellerId,
                CategoryId = code.CategoryId,
                ProgrammingLanguageId = code.ProgrammingLanguageId
            };

            return BaseApiResponse.OK(response);
        }

        /// <summary>
        /// Add a new code
        /// </summary>
        public async Task<BaseAPIResponse> CreateCode(CreateCodeDTO input)
        {
            //Validate Title
            if (string.IsNullOrWhiteSpace(input.Title))
                return BaseApiResponse.Error("Title is required.");

            //Check Seller
            var sellerExists = await UserRepository
                .GetAllAsNoTracking()
                .AnyAsync(x => x.Id == input.SellerId);

            if (!sellerExists)
                return BaseApiResponse.Error("Seller not found.");

            //Check Category
            var categoryExists = await UnitOfWork.CategoryRepository
                .GetAllAsNoTracking()
                .AnyAsync(x => x.Id == input.CategoryId);

            if (!categoryExists)
                return BaseApiResponse.Error("Category not found.");

            //Check Programming Language
            var languageExists = await ProgrammingLanguageRepository
                .GetAllAsNoTracking()
                .AnyAsync(x => x.Id == input.ProgrammingLanguageId);

            if (!languageExists)
                return BaseApiResponse.Error("Programming language not found.");

            //Create Code
            var code = new Code
            {
                Title = input.Title,
                Description = input.Description,
                Price = input.Price,
                Currency = input.Currency,
                SellerId = input.SellerId,
                CategoryId = input.CategoryId,
                ProgrammingLanguageId = input.ProgrammingLanguageId,
                DemoUrl = input.DemoUrl,
                PreviewImage = input.PreviewImage,
                ThumbnailUrl = input.ThumbnailUrl,
                Status = "PENDING", 
                Views = 0,
                Downloads = 0,
                AvgRating = 0,
                IsFeatured = false,
                LicenseType = input.LicenseType,

            };
            await CodeRepository.AddAsync(code);
            await CodeRepository.SaveChangesAsync();
            return BaseApiResponse.OK(code, "Code created successfully, waiting for approval." );
        }


        public async  Task<BaseAPIResponse> UpdateCode(int id, UpdateCodeDTO input)
        {
            var code = await CodeRepository.GetByIdAsync(id);

            if (code == null)
                return BaseApiResponse.NotFound("Code not found.");

            // Validate Category 
            if (input.CategoryId.HasValue)
            {
                var categoryExists = await CategoryRepository.GetAllAsNoTracking().AnyAsync(x => x.Id == input.CategoryId.Value);

                if (!categoryExists)
                    return BaseApiResponse.Error("Category not found.");

                code.CategoryId = input.CategoryId.Value;
            }

            // Validate ProgrammingLanguage 
            if (input.ProgrammingLanguageId.HasValue)
            {
                var languageExists = await ProgrammingLanguageRepository
                    .GetAllAsNoTracking()
                    .AnyAsync(x => x.Id == input.ProgrammingLanguageId.Value);

                if (!languageExists)
                    return BaseApiResponse.Error("Programming language not found.");

                code.ProgrammingLanguageId = input.ProgrammingLanguageId.Value;
            }

            // Update simple fields
            code.Title = input.Title ?? code.Title;
            code.Description = input.Description ?? code.Description;
            code.Price = input.Price ?? code.Price;
            code.Currency = input.Currency ?? code.Currency;
            code.LicenseType = input.LicenseType ?? code.LicenseType;
            code.ThumbnailUrl = input.ThumbnailUrl ?? code.ThumbnailUrl;
            code.DemoUrl = input.DemoUrl ?? code.DemoUrl;

            CodeRepository.Update(code);
            await CodeRepository.SaveChangesAsync();

            return BaseApiResponse.OK(code, "Code updated successfully.");
        }

        /// <summary>
        /// Delete code
        /// </summary>
        public async Task<BaseAPIResponse> DeleteCode(int id)
        {
            var code = await CodeRepository.GetByIdAsync(id);

            if (code == null)
                return BaseApiResponse.NotFound("Code not found.");

            // Soft delete
            code.Status = "DELETED";

            CodeRepository.Update(code);
            await CodeRepository.SaveChangesAsync();

            return BaseApiResponse.OK("Code deleted successfully.");
            
        }

        /// <summary>
        /// LikeCode
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="codeId"></param>
        /// <returns></returns>
        public async Task<BaseAPIResponse> LikeCode(int userId, int codeId)
        {
            // 1. Check code tồn tại
            var codeExists = await UnitOfWork.CodeRepository.GetAll().AnyAsync(x => x.Id == codeId && x.Status!= "DELETED") ;

            if (!codeExists)
                return BaseApiResponse.NotFound("Code not found.");

            // 2. Check đã like chưa
            var alreadyLiked = await CodeLikeRepository.GetAll().FirstOrDefaultAsync(x => x.UserId == userId && x.CodeId == codeId);

            if (alreadyLiked != null)
                return BaseApiResponse.Error("Code already liked.");

            
            var like = new CodeLike
            {
                UserId = userId,
                CodeId = codeId,
                CreatedAt = DateTime.UtcNow
                
            };

            await CodeLikeRepository.AddAsync(like);
            await CodeLikeRepository.SaveChangesAsync();

            return BaseApiResponse.OK(like, "Like successfully.");
        }

        /// <summary>
        /// UnlikeCode
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="codeId"></param>
        /// <returns></returns>
        public async Task<BaseAPIResponse> UnlikeCode(int userId, int codeId)
        {
            var like = await CodeLikeRepository.GetAll().FirstOrDefaultAsync(x => x.UserId == userId && x.CodeId == codeId);

            if (like == null)
            { 
                return BaseApiResponse.NotFound("Like not found.");
            }

            //like.DateLastMant = DateTime.UtcNow;
            CodeLikeRepository.Delete(like);
            await CodeLikeRepository.SaveChangesAsync();

            return BaseApiResponse.OK("Unlike successfully.");
        }


    }
}
