using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebBuySource.Dto.Request.users;
using WebBuySource.Dto.Response;
using WebBuySource.Interfaces;

namespace WebBuySource.Controllers
{
    [ApiController]
    [Route("api/v1/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICloudinaryService _cloudinaryService;

        public UserController(IUserService userService, ICloudinaryService cloudinaryService)
        {
            _userService = userService;
            _cloudinaryService = cloudinaryService;
        }

        /// <summary>
        /// Retrieve all users with optional pagination.
        /// </summary>
        /// <remarks>Only accessible by users with the Admin role.</remarks>
        /// <returns>List of users.</returns>
        /// <response code="200">Users successfully retrieved.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="401">User not authorized.</response>
        [HttpGet]
        public async Task<BaseAPIResponse> GetAllUsers([FromQuery] UserRequestDTO request)
        {
            return await _userService.GetAllUsers(request);
        }

        /// <summary>
        /// Retrieve a single user by ID.
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>User details.</returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<BaseAPIResponse> GetUserById(int id)
        {
            return await _userService.GetUserById(id);
        }
        /// <summary>
        /// Update an existing user's information.
        /// </summary>
        /// <remarks>Only accessible by users with the Admin role.</remarks>
        /// <param name="request">User data to update.</param>
        /// <returns>Updated user information.</returns>
        /// <response code="200">User successfully updated.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="404">User not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPut]
        [Authorize]
        public async Task<BaseAPIResponse> UpdateUser([FromQuery] UpdateUserRequestDTO request)
        {
            // If an avatar file is provided, upload it to Cloudinary
            if (request.AvatarFile != null)
            {
                var uploadResult = await _cloudinaryService.UploadImageAsync(request.AvatarFile);

                // If the upload fails, return the error response immediately
                if (uploadResult.Success != true)
                    return uploadResult;

                // Extract the uploaded image URL from the Items object
                if (uploadResult.Items != null)
                {
                    request.Avatar = ((dynamic)uploadResult.Items).Url;
                }
            }

            // Call the user service to update the user's data
            return await _userService.UpdateUser(request);
        }

        /// <summary>
        /// Upload an avatar image to Cloudinary.
        /// </summary>
        /// <remarks>Only accessible by users with the Admin role.</remarks>
        /// <param name="file">Image file to upload.</param>
        /// <returns>Uploaded image URL and success message.</returns>
        /// <response code="200">Image uploaded successfully.</response>
        /// <response code="400">No file provided.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost("upload-avatar")]
        [Authorize]
        public async Task<BaseAPIResponse> UploadAvatar(IFormFile file)
        {
            return await _cloudinaryService.UploadImageAsync(file);
        }

        /// <summary>
        /// Delete a user by ID.
        /// </summary>
        /// <remarks>Only accessible by users with the Admin role.</remarks>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>Success or error message.</returns>
        /// <response code="200">User successfully deleted.</response>
        /// <response code="404">User not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<BaseAPIResponse> DeleteUser(int id)
        {
            return await _userService.DeleteUser(id);
        }
    }
}
