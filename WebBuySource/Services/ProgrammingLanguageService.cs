using Microsoft.EntityFrameworkCore;
using WebBuySource.Dto.Request.ProgrammingLanguage;
using WebBuySource.Dto.Response;
using WebBuySource.Dto.Response.ProgrammingLanguage;
using WebBuySource.Interfaces;
using WebBuySource.Models;
using WebBuySource.Utilities;

namespace WebBuySource.Services
{
    public class ProgrammingLanguageService : BaseService, IProgrammingLanguageService
    {
        private IRepository<ProgrammingLanguage> ProgrammingLanguageRepository => UnitOfWork.ProgrammingLanguageRepository;

        public ProgrammingLanguageService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<BaseAPIResponse> GetAll(ProgrammingLanguageRequestDTO request)
        {
            var query = ProgrammingLanguageRepository
                .GetAllAsNoTracking()
                .Where(x => x.DeletedAt == null);

            var items = await query
                .OrderBy(x => x.Name)
                .Select(x => new ProgrammingLanguageResponseDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Code = x.Code,
                    Description = x.Description ?? ""
                })
                .ToListAsync();

            return BaseApiResponse.OK(items);
        }


        public async Task<BaseAPIResponse> Create(ProgrammingLanguageRequestDTO request)
        {
            if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Code))
                return BaseApiResponse.Error("Name and Code are required");

            var exists = await ProgrammingLanguageRepository.GetAllAsNoTracking()
                .AnyAsync(x => x.Code == request.Code);

            if (exists)
                return BaseApiResponse.Error("Programming language already exists");

            var entity = new ProgrammingLanguage
            {
                Name = request.Name,
                Code = request.Code,
                Description = request.Description
            };

            await ProgrammingLanguageRepository.AddAsync(entity);
            await ProgrammingLanguageRepository.SaveChangesAsync();

            return BaseApiResponse.OK(entity, "Created successfully");
        }

        public async Task<BaseAPIResponse> Update(ProgrammingLanguageRequestDTO request)
        {
            var entity = await ProgrammingLanguageRepository.GetByIdAsync(request.Id);
            if (entity == null)
                return BaseApiResponse.NotFound("Not found");

            entity.Name = request.Name ?? entity.Name;
            entity.Code = request.Code ?? entity.Code;
            entity.Description = request.Description ?? entity.Description;

            ProgrammingLanguageRepository.Update(entity);
            await ProgrammingLanguageRepository.SaveChangesAsync();

            return BaseApiResponse.OK(entity, "Updated successfully");
        }

        public async Task<BaseAPIResponse> Delete(int id)
        {
            var entity = await ProgrammingLanguageRepository.GetByIdAsync(id);
            if (entity == null)
                return BaseApiResponse.NotFound("Not found");

            entity.DeletedAt = DateTime.UtcNow;

            ProgrammingLanguageRepository.Update(entity);
            await ProgrammingLanguageRepository.SaveChangesAsync();

            return BaseApiResponse.OK("Deleted successfully");
        }

    }
}
