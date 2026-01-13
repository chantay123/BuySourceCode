using WebBuySource.Dto.Request.Permission;
using WebBuySource.Dto.Response;

namespace WebBuySource.Interfaces
{
    public interface IPermissionService
    {
        Task<BaseAPIResponse> GetAllPermission(PermissionRequestDTO request);
        Task<BaseAPIResponse> CreatePermission(CreatePermissionDTO dto);
        Task<BaseAPIResponse> UpdatePermission(int id, UpdatePermissionDTO dto);
        Task<BaseAPIResponse> DeletePermission(int id);

       
    }
}
