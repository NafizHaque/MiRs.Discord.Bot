using System.Net;
using System.Reflection.Metadata;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MiRs.API.Controllers;
using MiRs.Discord.Bot.Domain.Exceptions;
using NetCord;

namespace MiRs.Discord.Bot.API.Controllers
{
    /// <summary>
    /// This controller contains any calls relating to users.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class MainController : ApiControllerBase
    {
        private readonly ILogger<MainController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainController"/> class.
        /// </summary>
        /// <param name="logger">The logging interface.</param>
        public MainController(ILogger<MainController> logger)
        {
            _logger = logger;

        }

        /// <summary>
        /// Test
        /// </summary>
        /// <param name="username">Test.</param>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        /// <remarks>This call return user.</remarks>
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetUserStats(string username)
        {
            try
            {
                return Ok("test");

            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.CustomErrorMessage);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
