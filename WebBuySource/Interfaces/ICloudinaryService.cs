using WebBuySource.Dto.Response;

namespace WebBuySource.Interfaces
{
    public interface ICloudinaryService
    {
        Task<BaseAPIResponse> UploadImageAsync(IFormFile file);
    }
}

