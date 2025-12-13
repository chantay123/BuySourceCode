using WebBuySource.Dto.Request.Tag;
using WebBuySource.Dto.Response;

namespace WebBuySource.Interfaces
{
    public interface ITagService
    {
        Task<BaseAPIResponse> GetAll(TagRequestDTO request);
        Task<BaseAPIResponse> GetById(int id);
        Task<BaseAPIResponse> Create(CreateTagDTO input);
        Task<BaseAPIResponse> Update(int id, UpdateTagDTO input);
        Task<BaseAPIResponse> Delete(int id);
    }
}
