using WebBuySource.Dto.Request.CodeTag;
using WebBuySource.Dto.Response;

namespace WebBuySource.Interfaces
{
    public interface ICodeTagService
    {
        Task<BaseAPIResponse> GetAll(CodeTagRequestDTO request);
        Task<BaseAPIResponse> GetById(int id);
        Task<BaseAPIResponse> CreatecCodeTag(CreateCodeTagDTO input);
        Task<BaseAPIResponse> Delete(int id);
    }
}
