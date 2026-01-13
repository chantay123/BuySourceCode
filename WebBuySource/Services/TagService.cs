using Microsoft.EntityFrameworkCore;
using WebBuySource.Dto.Request.Tag;
using WebBuySource.Dto.Response;
using WebBuySource.Dto.Response.TagResponse;
using WebBuySource.Interfaces;
using WebBuySource.Models;
using WebBuySource.Utilities;

namespace WebBuySource.Services
{
    public class TagService : BaseService, ITagService
    {
        #region Reference repository
        private IRepository<Tag> TagRepository => UnitOfWork.TagRepository;
        #endregion

        #region Contructor
        public TagService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        #endregion


        #region GetAll Tag
        public async Task<BaseAPIResponse> GetAll(TagRequestDTO request)
        {
            var result = TagRepository
                .GetAllAsNoTracking()
                .Where(x => string.IsNullOrEmpty(request.Search)
                    || x.Name.Contains(request.Search))
                .Select(x => new TagResponse
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToList();

            return BaseApiResponse.OK(result);
        }
        #endregion


        #region Get Tag By Id
        public async Task<BaseAPIResponse> GetById(int id)
        {
            var tag = await TagRepository.GetByIdAsync(id);

            if (tag == null)
                return BaseApiResponse.NotFound("Tag not found");

            var response = new TagResponse
            {
                Id = tag.Id,
                Name = tag.Name
            };

            return BaseApiResponse.OK(response);
        }
        #endregion


        #region Create Tag
        public async Task<BaseAPIResponse> Create(CreateTagDTO input)
        {
            // Check duplicate name
            var exists = await TagRepository
                .GetAllAsNoTracking()
                .AnyAsync(x => x.Name == input.Name);

            if (exists)
                return BaseApiResponse.NotFound("Tag already exists");

            var tag = new Tag
            {
                Name = input.Name
            };

            await TagRepository.AddAsync(tag);
            await TagRepository.SaveChangesAsync();

            return BaseApiResponse.OK("Tag created successfully");
        }
        #endregion


        #region Update Tag
        public async Task<BaseAPIResponse> Update(int id, UpdateTagDTO input)
        {
            var tag = await TagRepository.GetByIdAsync(id);

            if (tag == null)
                return BaseApiResponse.NotFound("Tag not found");

            // Check duplicate
            var exists = await TagRepository
                .GetAllAsNoTracking()
                .AnyAsync(x => x.Name == input.Name && x.Id != id);

            if (exists)
                return BaseApiResponse.NotFound("Tag name already exists");

            tag.Name = input.Name;

            TagRepository.Update(tag);
            await TagRepository.SaveChangesAsync();

            return BaseApiResponse.OK("Updated successfully");
        }
        #endregion


        #region Delete Tag
        public async Task<BaseAPIResponse> Delete(int id)
        {
            var tag = await TagRepository.GetByIdAsync(id);

            if (tag == null)
                return BaseApiResponse.NotFound("Tag not found");

            TagRepository.Delete(tag);
            await TagRepository.SaveChangesAsync();

            return BaseApiResponse.OK("Deleted successfully");
        }
        #endregion
    }
}
