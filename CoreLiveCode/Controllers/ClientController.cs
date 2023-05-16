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


        /// <summary>
        /// Creates a new client.
        /// </summary>
        /// <param name="CreateRequest">The client to be created.</param>
        /// <returns>The created client.</returns>
        /// <response code="201">If the client is successfully created.</response>
        /// <response code="400">If the client request is null or invalid.</response>
        /// <response code="500">If there is an internal server error.</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Client
        ///     {
        ///        "name": "Client 1",
        ///        "email": "client1@example.com",
        ///        "phoneNumber": "123-456-7890",
        ///        "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
        ///     }
        /// </remarks>
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

        /// <summary>
        /// Retrieves a client by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the client.</param>
        /// <returns>The client with the provided unique identifier.</returns>
        /// <response code="200">If the client is successfully retrieved.</response>
        /// <response code="404">If the client with the provided identifier is not found.</response>
        /// <response code="500">If there is an internal server error.</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Client/{id}
        /// </remarks>
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
