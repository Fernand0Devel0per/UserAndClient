using CoreLiveCode.BLL.Interfaces;
using CoreLiveCode.Dtos.Client;
using CoreLiveCode.Dtos.User;
using CoreLiveCode.Helpers.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreLiveCode.Controllers
{
 
        [ApiController]
        [Route("api/[controller]")]
        public class ClientController : ControllerBase
        {
            private readonly IClientService _clientService;

            public ClientController(IClientService clientService)
            {
                _clientService = clientService;
            }


            [HttpPost]
            public async Task<IActionResult> CreateClient(ClientCreateRequest CreateRequest)
            {
                try
                {
                    var response = await _clientService.CreateAsync(CreateRequest);
                    return StatusCode(StatusCodes.Status201Created, response);
                }
                catch (Exception)
                {
                    return StatusCode(500, new { Message = "An error occurred while creating the product. Please try again later." });
                }
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetClient(Guid id)
            {
                try
                {
                    var client = await _clientService.GetByIdAsync(id);
                    return Ok(client);
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
