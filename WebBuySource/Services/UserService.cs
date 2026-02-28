using Microsoft.EntityFrameworkCore;
using WebBuySource.Dto.Request.users;
using WebBuySource.Dto.Response;
using WebBuySource.Dto.Response.JWTResponse;
using WebBuySource.Interfaces;
using WebBuySource.Models;
using WebBuySource.Utilities;
using WebBuySource.Utilities.Constants;

namespace WebBuySource.Services
{
    /// <summary>
    /// Service responsible for handling User-related operations,
    /// including CRUD and pagination.
    /// </summary>
    public class UserService : BaseService, IUserService
    {
        private IRepository<User> UserRepository => UnitOfWork.UserRepository;

        public UserService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// Retrieves all users with optional pagination.
        /// </summary>
        /// <param name="request">UserRequestDTO containing pagination info.</param>
        /// <returns>Paged list of users.</returns>
        public async Task<BaseAPIResponse> GetAllUsers(UserRequestDTO request)
        {
            // Select all users and map to UserResponseDTO
            var result = UserRepository.GetAll().Select(x => new UserResponseDTO
            {
                Id = x.Id,
                Username = x.Username,
                Fullname = x.Fullname,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                Avatar = x.Avatar,
                Gender = x.Gender,
            }).ToList();

            // Total number of users
            var total = result.Count;

            // Apply pagination
            var pagedResult = result
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            return BaseApiResponse.OK(pagedResult, total, request.PageSize);
        }

        /// <summary>
        /// Retrieves a single user by ID.
        /// </summary>
        /// <param name="id">User ID.</param>
        /// <returns>User details if found; NotFound otherwise.</returns>
        public async Task<BaseAPIResponse> GetUserById(int id)
        {
            var user = UserRepository.GetAll().FirstOrDefault(x => x.Id == id);

            if (user == null)
                return BaseApiResponse.NotFound();

            var result = new UserResponseDTO
            {
                Id = user.Id,
                Username = user.Username,
                Fullname = user.Fullname,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Avatar = user.Avatar,
                Gender = user.Gender,
            };
            return BaseApiResponse.OK(result);
        }

        /// <summary>
        /// Updates user information.
        /// </summary>
        /// <param name="request">UpdateUserRequestDTO containing update info.</param>
        /// <returns>Updated user data if successful; NotFound if user does not exist.</returns>
        public async Task<BaseAPIResponse> UpdateUser(UpdateUserRequestDTO request)
        {
            var user = await UserRepository.GetAll().FirstOrDefaultAsync(u => u.Id == request.Id);

            if (user == null)
                return BaseApiResponse.NotFound(MessageConstants.USER_NOT_FOUND);

            // Update fields if provided
            if (!string.IsNullOrWhiteSpace(request.Username))
                user.Username = request.Username.Trim();

            if (!string.IsNullOrWhiteSpace(request.Fullname))
                user.Fullname = request.Fullname.Trim();

            if (!string.IsNullOrWhiteSpace(request.Email))
                user.Email = request.Email.Trim();

            if (!string.IsNullOrWhiteSpace(request.PhoneNumber))
                user.PhoneNumber = request.PhoneNumber;

            if (request.Gender.HasValue)
                user.Gender = request.Gender;

            if (!string.IsNullOrWhiteSpace(request.Avatar))
                user.Avatar = request.Avatar;

            user.UpdatedAt = DateTime.UtcNow;

            UserRepository.Update(user);
            await UserRepository.SaveChangesAsync();

            var userDto = new UserResponseDTO
            {
                Id = user.Id,
                Username = user.Username,
                Fullname = user.Fullname,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Gender = user.Gender,
                Avatar = user.Avatar,
            };

            return BaseApiResponse.OK(userDto, MessageConstants.USER_UPDATED_SUCCESS);
        }

        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        /// <param name="id">User ID.</param>
        /// <returns>Success message if deleted; NotFound if user does not exist.</returns>
        public async Task<BaseAPIResponse> DeleteUser(int id)
        {
            var user = await UserRepository.GetAll().FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                return BaseApiResponse.NotFound(MessageConstants.USER_NOT_FOUND);

            UserRepository.Delete(user);
            await UserRepository.SaveChangesAsync();

            return BaseApiResponse.OK(MessageConstants.USER_DELETED_SUCCESS);
        }
    }
}
