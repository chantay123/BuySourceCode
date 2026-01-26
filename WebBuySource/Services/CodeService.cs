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

        private IRepository<ProgrammingLanguage> ProgrammingLanguageRepository => UnitOfWork.ProgrammingLanguageRepository;
        #endregion
        public CodeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// Get all codes
        /// </summary>
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

        public async Task<BaseAPIResponse> GetCodeById(int id)
        {
            var code = await CodeRepository.GetByIdAsync(id);

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
                ThumbnailUrl = code.ThumbnailUrl,
                Status = code.Status,
                Views = code.Views,
                Downloads = code.Downloads,
                AvgRating = code.AvgRating
            };

            return BaseApiResponse.OK(response);
        }

        /// <summary>
        /// Add a new code
        /// </summary>
        public async Task<BaseAPIResponse> CreateCode(CreateCodeDTO input)
        {
            if (string.IsNullOrWhiteSpace(input.Title))
                return BaseApiResponse.Error("Title is required.");

            var newCode = new Code
            {
                Title = input.Title,
                Description = input.Description,
                Price = input.Price,
                Currency = input.Currency,
                SellerId = input.SellerId,
                CategoryId = input.CategoryId,
                ProgrammingLanguageId = input.ProgrammingLanguageId,
                DemoUrl = input.DemoUrl,
                ThumbnailUrl = input.ThumbnailUrl,
                Status = "ACTIVE",
                Views = 0,
                Downloads = 0,
                AvgRating = 0,
                IsFeatured = false,
                LicenseType = input.LicenseType,
                CreatedAt = DateTime.UtcNow
            };

            await CodeRepository.AddAsync(newCode);
            await CodeRepository.SaveChangesAsync();

            var response = new CodeResponse
            {
                Id = newCode.Id,
                Title = newCode.Title,
                Description = newCode.Description ?? string.Empty,
                Price = newCode.Price,
                Currency = newCode.Currency,
                ThumbnailUrl = newCode.ThumbnailUrl,
                Status = newCode.Status,
            };

            return BaseApiResponse.OK(response, "Code created successfully.");
        }

        public async  Task<BaseAPIResponse> UpdateCode(int id, UpdateCodeDTO input)
        {
            var code = await CodeRepository.GetByIdAsync(id);

            if (code == null)
                return BaseApiResponse.NotFound("Code not found.");

            code.Title = input.Title ?? code.Title;
            code.Description = input.Description ?? code.Description;
            code.Price = input.Price ?? code.Price;
            code.Currency = input.Currency ?? code.Currency;
            code.CategoryId = input.CategoryId ?? code.CategoryId;
            code.ProgrammingLanguageId = input.ProgrammingLanguageId ?? code.ProgrammingLanguageId;
            code.LicenseType = input.LicenseType ?? code.LicenseType;
            code.ThumbnailUrl = input.ThumbnailUrl ?? code.ThumbnailUrl;
            code.DemoUrl = input.DemoUrl ?? code.DemoUrl;

            CodeRepository.Update(code);

            await CodeRepository.SaveChangesAsync();

            var response = new CodeResponse
            {
                Id = code.Id,
                Title = code.Title,
                Description = code.Description ?? string.Empty,
                Price = code.Price,
                Currency = code.Currency,
                ThumbnailUrl = code.ThumbnailUrl,
                Status = code.Status
            };

            return BaseApiResponse.OK(response, "Code updated successfully.");
        }

        /// <summary>
        /// Delete code
        /// </summary>
        public async Task<BaseAPIResponse> DeleteCode(int id)
        {
            var code = await CodeRepository.GetByIdAsync(id);

            if (code == null)
                return BaseApiResponse.NotFound("Code not found.");

            CodeRepository.Delete(code);
            await CodeRepository.SaveChangesAsync();

            return BaseApiResponse.OK("Code deleted successfully.");
        }
    }
}
