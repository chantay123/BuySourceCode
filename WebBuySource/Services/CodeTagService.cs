using Microsoft.EntityFrameworkCore;
using WebBuySource.Dto.Request.CodeTag;
using WebBuySource.Dto.Response;
using WebBuySource.Interfaces;
using WebBuySource.Models;
using WebBuySource.Utilities;

namespace WebBuySource.Services
{
    public class CodeTagService : BaseService, ICodeTagService
    {
        private IRepository<CodeTag> CodeTagRepository => UnitOfWork.CodeTagRepository;
        private IRepository<Code> CodeRepository => UnitOfWork.CodeRepository;
        private IRepository<Tag> TagRepository => UnitOfWork.TagRepository;

        public CodeTagService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        #region GetAll
        public async Task<BaseAPIResponse> GetAll(CodeTagRequestDTO request)
        {
            var result = await CodeTagRepository
                .GetAllAsNoTracking()
                .Include(ct => ct.Code)
                .Include(ct => ct.Tag)
                .Select(ct => new
                {
                    ct.Id,
                    CodeId = ct.CodeId,
                    TagId = ct.TagId,
                    TagName = ct.Tag.Name,
                    CodeTitle = ct.Code.Title
                })
                .ToListAsync();

            return BaseApiResponse.OK(result);
        }
        #endregion

        #region GetById
        public async Task<BaseAPIResponse> GetById(int id)
        {
            var item = await CodeTagRepository
                .GetAllAsNoTracking()
                .Include(ct => ct.Code)
                .Include(ct => ct.Tag)
                .Where(ct => ct.Id == id)
                .Select(ct => new
                {
                    ct.Id,
                    ct.CodeId,
                    ct.TagId,
                    TagName = ct.Tag.Name,
                    CodeTitle = ct.Code.Title
                })
                .FirstOrDefaultAsync();

            if (item == null)
                return BaseApiResponse.NotFound("CodeTag not found.");

            return BaseApiResponse.OK(item);
        }
        #endregion

        #region Create
        public async Task<BaseAPIResponse> CreatecCodeTag(CreateCodeTagDTO input)
        {
            // Validate Code
            var codeExists = await CodeRepository.GetAllAsNoTracking()
                .AnyAsync(x => x.Id == input.CodeId);

            if (!codeExists)
                return BaseApiResponse.NotFound("Code does not exist.");

            // Validate Tag
            var tagExists = await TagRepository.GetAllAsNoTracking()
                .AnyAsync(x => x.Id == input.TagId);

            if (!tagExists)
                return BaseApiResponse.NotFound("Tag does not exist.");

            // Check duplicate
            var exists = await CodeTagRepository.GetAllAsNoTracking()
                .AnyAsync(x => x.CodeId == input.CodeId && x.TagId == input.TagId);

            if (exists)
                return BaseApiResponse.NotFound("This Code already has this Tag.");

            var item = new CodeTag
            {
                CodeId = input.CodeId,
                TagId = input.TagId
            };

            await CodeTagRepository.AddAsync(item);
            await CodeTagRepository.SaveChangesAsync();

            return BaseApiResponse.OK(item, "CodeTag created successfully.");
        }
        #endregion

        #region Delete
        public async Task<BaseAPIResponse> Delete(int id)
        {
            var item = await CodeTagRepository.GetByIdAsync(id);

            if (item == null)
                return BaseApiResponse.NotFound("CodeTag not found.");

            CodeTagRepository.Delete(item);
            await CodeTagRepository.SaveChangesAsync();

            return BaseApiResponse.OK("CodeTag deleted successfully.");
        }

       
        #endregion
    }
}
