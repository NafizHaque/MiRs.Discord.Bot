using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MiRs.API.Controllers;
using MiRs.Discord.Bot.Domain.Entities;
using MiRs.Discord.Bot.Domain.Exceptions;
using MiRs.Discord.Bot.Gateway.MiRsClient;
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
    public class RestController : ApiControllerBase
    {
        private readonly ILogger<RestController> _logger;
        private readonly RestClient _restclient;
        private readonly IMiRsAdminClient _miRsAdminClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="RestController"/> class.
        /// </summary>
        /// <param name="logger">The logging interface.</param>
        public RestController(ILogger<RestController> logger, RestClient restClient, IMiRsAdminClient miRsAdminClient)
        {
            _logger = logger;
            _restclient = restClient;
            _miRsAdminClient = miRsAdminClient;

        }

        /// <summary>
        /// Test
        /// </summary>
        /// <returns><see cref="Task"/> representing the asynchronous operation.</returns>
        /// <remarks>This call return user.</remarks>
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> SendEventWinner(EventWinner eventWinner)
        {
            try
            {
                MessageProperties messageProp = new MessageProperties
                {
                    Content = $"The Winner is... Team {eventWinner.Team.TeamName}"
                };

                RestMessage message = await _restclient.SendMessageAsync(eventWinner.Perms.ChannelId, messageProp);

                _restclient.ModifyInteractionResponseAsync()
                return Ok();

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
