using NetCord.Services.ApplicationCommands;
using MediatR;

namespace MiRs.Discord.Bot.Mediator.Model.Home
{
    public class HomeRequest : IRequest<HomeResponse>
    {
        /// <summary>
        /// Gets or sets the Username.
        /// </summary>
        public ApplicationCommandContext? Context { get; set; }
    }
}
