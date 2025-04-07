using System.Net;
using System.Reflection.Metadata;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MiRs.API.Controllers;
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
        /// Logs Discord user message.
        /// </summary>
        /// <response code="200">Returns a list of users in json format.</response>
        [HttpGet]
        public async Task<IActionResult> LogUserMessage()
        {
            return Ok("Message event handled.");
        }
    }
}
