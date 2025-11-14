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

        public UserController(IUserService userService)
        {
            _userService = userService;
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
        public async Task<BaseAPIResponse> UpdateUser([FromBody] UpdateUserRequestDTO request)
        {
            return await _userService.UpdateUser(request);
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
        public async Task<BaseAPIResponse> DeleteUser(int id)
        {
            return await _userService.DeleteUser(id);
        }


    }
}
