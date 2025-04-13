using MediatR;
using NetCord.Services.ApplicationCommands;
using Microsoft.Extensions.DependencyInjection;
using NetCord.Rest;
using NetCord;

namespace MiRs.Discord.Bot.API.Controllers.Commands
{
    /// <summary>
    ///  Base module that gives access to MediatR object
    /// </summary>
    public abstract class BaseModule : ApplicationCommandModule<ApplicationCommandContext>
    {
        /// <summary>
        ///  gets the Mediator object
        /// </summary>
        protected ISender Mediator { get; }

        /// <summary>
        /// Creates MediatR object
        /// </summary>
        protected BaseModule(ISender mediator)
        {
            Mediator = mediator;
            var x = Context;
        }

        /// <summary>
        /// Discord app command to see user Id
        /// </summary>
        [UserCommand("ID")]
        public string Id(User user)
        {
            return user.Id.ToString();
        }

        /// <summary>
        /// Discord app command to see message create date
        /// </summary>
        [MessageCommand("Timestamp")]
        public string Timestamp(RestMessage message)
        {
            return message.CreatedAt.ToString();
        }

        /// <summary>
        /// Simple ping pong command
        /// </summary>
        [SlashCommand("ping", "Ping!")]
        public async Task<string> Pong()
        {
            return await Task.FromResult("Ping!");
        }
    }
}
