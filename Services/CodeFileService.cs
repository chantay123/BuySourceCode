using Microsoft.EntityFrameworkCore;
using Sprache;
using WebBuySource.Dto.Request.CodeFile;
using WebBuySource.Dto.Response;
using WebBuySource.Dto.Response.CodeFile;
using WebBuySource.Interfaces;
using WebBuySource.Models;
using WebBuySource.Utilities;

namespace WebBuySource.Services
{
    public class CodeFileService : BaseService, ICodeFileService
    {

        private IRepository<CodeFile> CodeFileRepository => UnitOfWork.CodeFileRepository;
        public CodeFileService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<BaseAPIResponse> GetFilesByCode(int codeId)
        {
            var files = await CodeFileRepository
                 .GetAllAsNoTracking()
                 .Where(f => f.CodeId == codeId)
                 .Select(f => new CodeFileResponse
                 {
                     Id = f.Id,
                     CodeId = f.CodeId,
                     FileUrl = f.FileUrl,
                     FileSize = f.FileSize,
                     Version = f.Version,
                     IsCurrent = f.IsCurrent,
                     CreatedAt = f.CreatedAt
                 }).ToListAsync();

            return BaseApiResponse.OK(files);
        }
        public async Task<BaseAPIResponse> CreateFile(CreateCodeFileDTO input)
        {
            if (input.IsCurrent)
            {
                var oldFiles = await CodeFileRepository
                    .GetAllAsNoTracking()
                    .Where(f => f.CodeId == input.CodeId && f.IsCurrent)
                    .ToListAsync();

                foreach (var f in oldFiles)
                {
                    f.IsCurrent = false;
                    CodeFileRepository.Update(f);
                }
            }

            var file = new CodeFile
            {
                CodeId = input.CodeId,
                FileUrl = input.FileUrl,
                FileSize = input.FileSize,
                Version = input.Version,
                IsCurrent = input.IsCurrent,
                CreatedAt = DateTime.UtcNow
            };

            await CodeFileRepository.AddAsync(file);
            await CodeFileRepository.SaveChangesAsync();

            return BaseApiResponse.OK(file, "File created successfully.");
        }


        public async Task<BaseAPIResponse> UpdateFile(int fileId, UpdateCodeFileDTO request)
        {
            var file = await CodeFileRepository.GetByIdAsync(fileId);
            if (file == null)
                return BaseApiResponse.NotFound("File not found.");

            
            if (request.IsCurrent == true)
            {
                var oldFiles = await CodeFileRepository
                    .GetAllAsNoTracking()
                    .Where(f => f.CodeId == file.CodeId && f.IsCurrent)
                    .ToListAsync();

                foreach (var old in oldFiles)
                {
                    old.IsCurrent = false;
                    CodeFileRepository.Update(old);
                }
            }

            file.FileUrl = request.FileUrl ?? file.FileUrl;
            file.FileSize = request.FileSize ?? file.FileSize;
            file.Version = request.Version ?? file.Version;
            file.IsCurrent = request.IsCurrent ?? file.IsCurrent;
                
            CodeFileRepository.Update(file);
            await CodeFileRepository.SaveChangesAsync();

            return BaseApiResponse.OK(file, "File updated successfully.");
        }

        public async Task<BaseAPIResponse> DeleteFile(int fileId)
        {
            var file = await CodeFileRepository.GetByIdAsync(fileId);

            if (file == null)
                return BaseApiResponse.NotFound("File not found.");

            CodeFileRepository.Delete(file);
            await CodeFileRepository.SaveChangesAsync();

            return BaseApiResponse.OK("File deleted successfully.");
        }

       

    }
}
