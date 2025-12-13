using Microsoft.AspNetCore.Mvc;
using WebBuySource.Dto.Request.Tag;
using WebBuySource.Dto.Response;
using WebBuySource.Interfaces;

namespace WebBuySource.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TagController : Controller
    {
        #region Reference services
        private readonly ITagService _tagService;
        #endregion

        #region Constructor
        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }
        #endregion

        #region Get All Tags
        /// <summary>
        /// Get all tags
        /// </summary>
        /// <param name="request">Filter condition</param>
        [HttpGet]
        public async Task<BaseAPIResponse> GetAll([FromQuery] TagRequestDTO request)
        {
            return await _tagService.GetAll(request);
        }
        #endregion

        #region Get Tag By Id
        [HttpGet("{id}")]
        public async Task<BaseAPIResponse> GetById(int id)
        {
            return await _tagService.GetById(id);
        }
        #endregion

        #region Create Tag
        [HttpPost]
        public async Task<BaseAPIResponse> Create([FromBody] CreateTagDTO input)
        {
            return await _tagService.Create(input);
        }
        #endregion

        #region Update Tag
        [HttpPut("{id}")]
        public async Task<BaseAPIResponse> Update(int id, [FromBody] UpdateTagDTO input)
        {
            return await _tagService.Update(id, input);
        }
        #endregion

        #region Delete Tag
        [HttpDelete("{id}")]
        public async Task<BaseAPIResponse> Delete(int id)
        {
            return await _tagService.Delete(id);
        }
        #endregion
    }
}
