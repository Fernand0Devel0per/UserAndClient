using CoreLiveCode.BLL.Interfaces;
using CoreLiveCode.Dtos.User;
using CoreLiveCode.Helpers.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreLiveCode.Controllers
{
    public class UserController : Controller
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

            [HttpPost]
            public async Task<IActionResult> CreateProduct(UserCreateRequest CreateRequest)
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

            [HttpGet("{id}")]
            public async Task<IActionResult> GetUser(Guid id)
            {
                try
                {
                    var user = await _userService.GetUserById(id);
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
    
}
