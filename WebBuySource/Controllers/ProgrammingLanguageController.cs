using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebBuySource.Dto.Request.ProgrammingLanguage;
using WebBuySource.Dto.Response;
using WebBuySource.Interfaces;

namespace WebBuySource.Controllers
{
    [ApiController]
    [Route("api/v1/programming-language")]
    public class ProgrammingLanguageController : ControllerBase
    {
        private readonly IProgrammingLanguageService _programmingLanguageService;

        #region Constructor
        public ProgrammingLanguageController(
            IProgrammingLanguageService programmingLanguageService)
        {
            _programmingLanguageService = programmingLanguageService;
        }
        #endregion

        /// <summary>
        /// Retrieve all programming languages.
        /// </summary>
        /// <response code="200">Retrieved successfully</response>
        /// <response code="400">Invalid request</response>
        [HttpGet]
        public async Task<BaseAPIResponse> GetAll(
            [FromQuery] ProgrammingLanguageRequestDTO request)
        {
            return await _programmingLanguageService.GetAll(request);
        }

        /// <summary>
        /// Create a new programming language.
        /// </summary>
        /// <remarks>Only accessible by Admin</remarks>
        /// <response code="200">Created successfully</response>
        /// <response code="400">Invalid data</response>
        /// <response code="401">Unauthorized</response>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<BaseAPIResponse> Create(
            [FromBody] ProgrammingLanguageRequestDTO input)
        {
            return await _programmingLanguageService.Create(input);
        }

        /// <summary>
        /// Update a programming language.
        /// </summary>
        /// <remarks>Only accessible by Admin</remarks>
        /// <response code="200">Updated successfully</response>
        /// <response code="400">Invalid data</response>
        /// <response code="401">Unauthorized</response>
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<BaseAPIResponse> Update(
            [FromBody] ProgrammingLanguageRequestDTO request)
        {
            return await _programmingLanguageService.Update(request);
        }

        /// <summary>
        /// Delete a programming language.
        /// </summary>
        /// <remarks>Only accessible by Admin</remarks>
        /// <response code="200">Deleted successfully</response>
        /// <response code="400">Invalid id</response>
        /// <response code="401">Unauthorized</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<BaseAPIResponse> Delete(int id)
        {
            return await _programmingLanguageService.Delete(id);
        }
    }
}
