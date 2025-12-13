using Microsoft.AspNetCore.Mvc;
using WebBuySource.Dto.Request.Code;
using WebBuySource.Dto.Response;
using WebBuySource.Interfaces;

namespace WebBuySource.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CodeController : ControllerBase
    {
        private readonly ICodeService _codeService;

        public CodeController(ICodeService codeService)
        {
            _codeService = codeService;
        }

        /// <summary>
        /// Get a list of all Codes with optional filters and pagination.
        /// Example: GET /codes?page=1&pageSize=10
        /// </summary>
        [HttpGet]
        public async Task<BaseAPIResponse> GetAllCodes([FromQuery] CodeRequestDTO request)
        {
            return await _codeService.GetAllCodes(request); 
        }

        /// <summary>
        /// Get a single Code by its ID.
        /// Example: GET /codes/5
        /// </summary>
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
        public async Task<BaseAPIResponse> UpdateCode(int id, [FromBody] UpdateCodeDTO dto)
        {
           
            return await _codeService.UpdateCode(id, dto); 
        }

        /// <summary>
        /// Delete a Code by its ID.
        /// Example: DELETE /codes/3
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<BaseAPIResponse> DeleteCode(int id)
        {
            return await _codeService.DeleteCode(id); ;
        }
    }
}
