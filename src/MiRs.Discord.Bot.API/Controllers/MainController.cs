using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MiRs.API.Controllers;
using MiRs.Discord.Bot.Domain.Exceptions;
using NetCord;
using NetCord.Rest;
using System.Net;

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
        private readonly RestClient _restclient;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainController"/> class.
        /// </summary>
        /// <param name="logger">The logging interface.</param>
        public MainController(ILogger<MainController> logger, RestClient restClient)
        {
            _logger = logger;
            _restclient = restClient;

        }

        /// <summary>
        /// Test
        /// </summary>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        /// <remarks>This call return user.</remarks>
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> SendEventWinner()
        {
            try
            {
                MessageProperties message = new MessageProperties
                {
                    Content = "Hello Discord! This message is sent via REST API. For <@&1471171292668493937>"
                };

                await _restclient.SendMessageAsync(1471129626012160191, message);
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
