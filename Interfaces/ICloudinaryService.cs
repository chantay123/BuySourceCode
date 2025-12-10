using WebBuySource.Dto.Response.JWTResponse;

namespace WebBuySource.Interfaces
{
    public interface ICloudinaryService
    {
        Task<BaseAPIResponse> UploadImageAsync(IFormFile file);
    }
}

