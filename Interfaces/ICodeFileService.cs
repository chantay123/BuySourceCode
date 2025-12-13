using WebBuySource.Dto.Request.CodeFile;
using WebBuySource.Dto.Response;

namespace WebBuySource.Interfaces
{
    public interface ICodeFileService
    {
        Task<BaseAPIResponse> CreateFile(CreateCodeFileDTO input);
        Task<BaseAPIResponse> GetFilesByCode(int codeId);
        Task<BaseAPIResponse> UpdateFile(int fileId, UpdateCodeFileDTO request);
        Task<BaseAPIResponse> DeleteFile(int fileId);
    }
}