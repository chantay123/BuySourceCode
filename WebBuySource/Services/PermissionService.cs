using Microsoft.EntityFrameworkCore;
using WebBuySource.Dto.Request.Permission;
using WebBuySource.Dto.Response;
using WebBuySource.Dto.Response.PermissionResponse;
using WebBuySource.Interfaces;
using WebBuySource.Models;
using WebBuySource.Utilities;

namespace WebBuySource.Services
{
    public class PermissionService : BaseService, IPermissionService
    {
        private IRepository<Permission> PermissionRepository
            => UnitOfWork.PermissionRepository;

        private IRepository<Role> RoleRepository
            => UnitOfWork.RoleRepository;

        private IRepository<RolePermission> RolePermissionRepository
            => UnitOfWork.RolePermissionRepository;

        public PermissionService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// Get all permissions
        /// </summary>
        public async Task<BaseAPIResponse> GetAllPermission(PermissionRequestDTO request)
        {
            var query = PermissionRepository
                .GetAllAsNoTracking()
                .Where(x => x.DeletedAt == null);

            if (!string.IsNullOrWhiteSpace(request.Keyword))
            {
                query = query.Where(x =>
                    x.Name.Contains(request.Keyword) ||
                    x.Module.Contains(request.Keyword));
            }

            var data = await query
                .Select(x => new PermissionResponseDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Module = x.Module,
                    Action = x.Action,
                    Path = x.Path,
                    Method = x.Method
                })
                .ToListAsync();

            return BaseApiResponse.OK(data);
        }

        /// <summary>
        /// Create permission
        /// </summary>
        public async Task<BaseAPIResponse> CreatePermission(CreatePermissionDTO dto)
        {
            var exists = await PermissionRepository.FirstOrDefaultAsync(x =>
                x.Name == dto.Name && x.DeletedAt == null);

            if (exists != null)
                return BaseApiResponse.Error("PERMISSION_ALREADY_EXISTS");

            var permission = new Permission
            {
                Name = dto.Name,
                Module = dto.Module,
                Action = dto.Action,
                Path = dto.Path,
                Method = dto.Method
            };

            await PermissionRepository.AddAsync(permission);
            await PermissionRepository.SaveChangesAsync();

            return BaseApiResponse.OK();
        }

        /// <summary>
        /// Update permission
        /// </summary>
        public async Task<BaseAPIResponse> UpdatePermission(int id, UpdatePermissionDTO dto)
        {
            var permission = await PermissionRepository
                .FirstOrDefaultAsync(x => x.Id == id && x.DeletedAt == null);

            if (permission == null)
                return BaseApiResponse.NotFound("PERMISSION_NOT_FOUND");

            permission.Name = dto.Name;
            permission.Module = dto.Module;
            permission.Action = dto.Action;
            permission.Path = dto.Path;
            permission.Method = dto.Method;

            await PermissionRepository.SaveChangesAsync();
            return BaseApiResponse.OK();
        }

        /// <summary>
        /// Soft delete permission
        /// </summary>
        public async Task<BaseAPIResponse> DeletePermission(int id)
        {
            var permission = await PermissionRepository
                .FirstOrDefaultAsync(x => x.Id == id && x.DeletedAt == null);

            if (permission == null)
                return BaseApiResponse.NotFound("PERMISSION_NOT_FOUND");

            permission.DeletedAt = DateTime.UtcNow;
            await PermissionRepository.SaveChangesAsync();

            return BaseApiResponse.OK();
        }

        
    }
}
