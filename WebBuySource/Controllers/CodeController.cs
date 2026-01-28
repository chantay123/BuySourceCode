using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebBuySource.Dto.Request.Code;
using WebBuySource.Dto.Response;
using WebBuySource.Interfaces;
using WebBuySource.Models;

namespace WebBuySource.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CodeController : ControllerBase
    {
        private readonly ICodeService _codeService;


        public CodeController(ICodeService codeService, IUserService userService)
        {
            _codeService = codeService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<BaseAPIResponse> GetAllCodes([FromQuery] CodeRequestDTO request)
        {
            return await _codeService.GetAllCodes(request);
        }

        /// <summary>
        /// GetCodeById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<BaseAPIResponse> GetCodeById(int id)
        {

            return await _codeService.GetCodeById(id);
        }

        /// <summary>
        /// Create a new Code record.
        /// Example: POST /codes
        /// Body: CreateCodeDTO
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<BaseAPIResponse> AddCode([FromBody] CreateCodeDTO dto)
        {
            return await _codeService.CreateCode(dto);
        }

        /// <summary>
        /// Update an existing Code by its ID.
        /// Example: PUT /codes/7
        /// Body: UpdateCodeDTO
        /// Note: ID is taken from the URL, not from the body.
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<BaseAPIResponse> UpdateCode(int id, [FromBody] UpdateCodeDTO dto)
        {

            return await _codeService.UpdateCode(id, dto);
        }

        /// <summary>
        /// Delete a Code by its ID.
        /// Example: DELETE /codes/3
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<BaseAPIResponse> DeleteCode(int id)
        {
            return await _codeService.DeleteCode(id); ;
        }

        /// <summary>
        /// Like a code
        /// POST /api/v1/code/{id}/like
        /// </summary>
        [Authorize]
        [HttpPost("{id}/like")]
        public async Task<BaseAPIResponse> LikeCode(int id)
        {
            var userId = int.Parse(User.FindFirst("id")!.Value);
            return await _codeService.LikeCode(userId, id);
        }

        /// <summary>
        /// Unlike a code
        /// POST /api/v1/code/{id}/unlike
        /// </summary>
        [Authorize]
        [HttpPost("{id}/unlike")]
        public async Task<BaseAPIResponse> UnlikeCode(int id)
        {
            var userId = int.Parse(User.FindFirst("id")!.Value);
            return await _codeService.UnlikeCode(userId, id);
        }

    }
}
