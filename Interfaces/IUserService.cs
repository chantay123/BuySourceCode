using WebBuySource.Dto.Request.users;
using WebBuySource.Dto.Response.JWTResponse;

namespace WebBuySource.Interfaces
{
    public interface IUserService
    {

        Task<BaseAPIResponse> GetAllUsers(UserRequestDTO request);

        Task<BaseAPIResponse> GetUserById(int id);

        Task<BaseAPIResponse> UpdateUser(UpdateUserRequestDTO request);

        Task<BaseAPIResponse> DeleteUser(int id);

    }
}
