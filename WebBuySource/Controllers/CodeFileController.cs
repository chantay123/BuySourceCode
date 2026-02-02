using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebBuySource.Dto.Request.CodeFile;
using WebBuySource.Dto.Response;
using WebBuySource.Interfaces;

namespace WebBuySource.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CodeFileController : Controller
    {
        #region Reference services
        /// <summary> 
        /// The CodeFile service
        /// </summary>
        private readonly ICodeFileService _codeFileService;
        #endregion

        #region Constructor
        public CodeFileController(ICodeFileService codeFileService)
        {
            _codeFileService = codeFileService;
        }
        #endregion

        #region Get Code Files
        /// <summary>
        /// Get all files of a code
        /// </summary>
        /// <param name="codeId">ID of the code</param>
        /// <returns>List of files</returns>
        /// <response code="200">Successfully retrieved files.</response>
        /// <response code="400">Invalid input.</response>
        [HttpGet]
        public async Task<BaseAPIResponse> GetFilesByCode([FromQuery] int codeId)
        {
            return await _codeFileService.GetFilesByCode(codeId);
        }
        #endregion

        #region Create Code File
        /// <summary>
        /// Create new code file
        /// </summary>
        /// <param name="input">File data</param>
        /// <returns>Created file</returns>
        /// <response code="200">File created successfully.</response>
        /// <response code="400">Invalid data.</response>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<BaseAPIResponse> CreateFile([FromBody] CreateCodeFileDTO input)
        {
            return await _codeFileService.CreateFile(input);
        }
        #endregion

        #region Update Code File
        /// <summary>
        /// Update a code file
        /// </summary>
        /// <param name="fileId">File ID</param>
        /// <param name="request">Update data</param>
        /// <returns>Updated file</returns>
        /// <response code="200">Updated successfully.</response>
        /// <response code="404">File not found.</response>
        [HttpPut("{fileId}")]
        [Authorize(Roles = "Admin")]
        public async Task<BaseAPIResponse> UpdateFile(int fileId, [FromBody] UpdateCodeFileDTO request)
        {
            return await _codeFileService.UpdateFile(fileId, request);
        }
        #endregion

        #region Delete Code File
        /// <summary>
        /// Delete a code file
        /// </summary>
        /// <param name="fileId">ID of the file</param>
        /// <returns>Deleted status</returns>
        /// <response code="200">File deleted successfully.</response>
        /// <response code="404">File not found.</response>
        [HttpDelete("{fileId}")]
        [Authorize(Roles = "Admin")]
        public async Task<BaseAPIResponse> DeleteFile(int fileId)
        {
            return await _codeFileService.DeleteFile(fileId);
        }
        #endregion
    }
}
