using WebBuySource.Dto.Request.ProgrammingLanguage;
using WebBuySource.Dto.Response;

namespace WebBuySource.Interfaces
{
    public interface IProgrammingLanguageService
    {

        Task<BaseAPIResponse> GetAll(ProgrammingLanguageRequestDTO request);
        Task<BaseAPIResponse> Create(ProgrammingLanguageRequestDTO request);
        Task<BaseAPIResponse> Update(ProgrammingLanguageRequestDTO request);
        Task<BaseAPIResponse> Delete(int id);
    }
}
