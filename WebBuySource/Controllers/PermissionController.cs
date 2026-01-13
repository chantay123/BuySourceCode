using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebBuySource.Dto.Request.Permission;
using WebBuySource.Dto.Response;
using WebBuySource.Interfaces;

namespace WebBuySource.Controllers
{
    [ApiController]
    [Route("api/v1/permissions")]
    //[Authorize(Roles = "Admin")]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        /// <summary>
        /// Get all permissions
        /// </summary>
        /// <param name="request">Filter permission</param>
        /// <returns>List permissions</returns>
        [HttpGet]
        public async Task<BaseAPIResponse> GetAll([FromQuery] PermissionRequestDTO request)
        {
            return await _permissionService.GetAllPermission(request);
        }

        /// <summary>
        /// Create new permission
        /// </summary>
        /// <param name="dto">Permission data</param>
        /// <returns>Success or error</returns>
        [HttpPost]
        public async Task<BaseAPIResponse> Create([FromBody] CreatePermissionDTO dto)
        {
            return await _permissionService.CreatePermission(dto);
        }

        /// <summary>
        /// Update permission
        /// </summary>
        /// <param name="id">Permission ID</param>
        /// <param name="dto">Permission data</param>
        /// <returns>Success or error</returns>
        [HttpPut("{id}")]
        public async Task<BaseAPIResponse> Update(int id, [FromBody] UpdatePermissionDTO dto)
        {
            return await _permissionService.UpdatePermission(id, dto);
        }

        /// <summary>
        /// Delete permission (soft delete)
        /// </summary>
        /// <param name="id">Permission ID</param>
        /// <returns>Success or error</returns>
        [HttpDelete("{id}")]
        public async Task<BaseAPIResponse> Delete(int id)
        {
            return await _permissionService.DeletePermission(id);
        }
    }
}
