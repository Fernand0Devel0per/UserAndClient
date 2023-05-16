using CoreLiveCode.BLL.Interfaces;
using CoreLiveCode.Dtos.User;
using CoreLiveCode.Helpers.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreLiveCode.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="CreateRequest">The user to be created.</param>
        /// <returns>The created user.</returns>
        /// <response code="201">If the user is successfully created.</response>
        /// <response code="400">If the user request is null or invalid.</response>
        /// <response code="500">If there is an internal server error.</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /User
        ///     {
        ///        "name": "User 1",
        ///        "email": "user1@example.com"
        ///     }
        /// </remarks>
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserCreateRequest CreateRequest)
        {
            try
            {
                var response = await _userService.CreateAsync(CreateRequest);
                return StatusCode(StatusCodes.Status201Created, response);
            }
            catch (Exception)
            {
                return StatusCode(500, new { Message = "An error occurred while creating the product. Please try again later." });
            }
        }

        /// <summary>
        /// Retrieves a user by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <returns>The user with the provided unique identifier.</returns>
        /// <response code="200">If the user is successfully retrieved.</response>
        /// <response code="404">If the user with the provided identifier is not found.</response>
        /// <response code="500">If there is an internal server error.</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /User/{id}
        /// </remarks>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            try
            {
                var user = await _userService.GetByIdAsync(id);
                return Ok(user);
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred while updating the product." });
            }
        }

    }

}
