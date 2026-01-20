using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebBuySource.Dto.Request.Comment;
using WebBuySource.Dto.Response;
using WebBuySource.Interfaces;

namespace WebBuySource.Controllers
{
    [ApiController]
    [Route("api/v1/comments")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        #region Constructor
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        #endregion

        /// <summary>
        /// Create a new comment
        /// </summary>
        /// <response code="200">Comment created successfully</response>
        /// <response code="400">Invalid request</response>
        /// <response code="401">Unauthorized</response>
        [HttpPost]
        [Authorize]
        public async Task<BaseAPIResponse> CreateComment(
            [FromBody] CommentRequestDTO request
        )
        {
            // Lấy userId từ JWT (KHÔNG lấy từ body)
            int userId = int.Parse(User.FindFirst("id")!.Value);

            return await _commentService.CreateComment(userId, request);
        }

        /// <summary>
        /// Get comments by code
        /// </summary>
        /// <response code="200">Success</response>
        /// <response code="400">Invalid CodeId</response>
        [HttpGet("code/{codeId}")]
        public async Task<BaseAPIResponse> GetCommentsByCode(
            [FromBody] CommentRequestDTO request

        )
        {
            

            return await _commentService.GetCommentsByCode(request);
        }
    }
}
