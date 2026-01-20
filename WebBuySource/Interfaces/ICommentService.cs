using WebBuySource.Dto.Request.Comment;
using WebBuySource.Dto.Response;

namespace WebBuySource.Interfaces
{
    public interface ICommentService
    {
        /// <summary>
        /// create  comment
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<BaseAPIResponse> CreateComment(int userId,CommentRequestDTO request);

        /// <summary>
        /// get comment by code
        /// </summary>
        /// <param name="codeId"></param>
        /// <returns></returns>
        Task<BaseAPIResponse> GetCommentsByCode(CommentRequestDTO request);
    }
}
