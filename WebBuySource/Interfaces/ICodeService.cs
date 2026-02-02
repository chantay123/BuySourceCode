using WebBuySource.Dto.Request.Code;
using WebBuySource.Dto.Response;

namespace WebBuySource.Interfaces
{
    public interface ICodeService
    {
        /// <summary>
        /// Get All Codes
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseAPIResponse> GetAllCodes(CodeRequestDTO request);

        /// <summary>
        /// Get Code by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BaseAPIResponse> GetCodeById(int id);

        /// <summary>
        /// Add new Code
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<BaseAPIResponse> CreateCode(CreateCodeDTO input);

        /// <summary>
        /// Update Code
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<BaseAPIResponse> UpdateCode(int id, UpdateCodeDTO input);

        /// <summary>
        /// Delete Code
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BaseAPIResponse> DeleteCode(int id);

        /// <summary>
        /// like code
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="codeId"></param>
        /// <returns></returns>
        Task<BaseAPIResponse> LikeCode(int userId, int codeId);

        /// <summary>
        /// unlike code 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="codeId"></param>
        /// <returns></returns>
        Task<BaseAPIResponse> UnlikeCode(int userId, int codeId);
    }
}
