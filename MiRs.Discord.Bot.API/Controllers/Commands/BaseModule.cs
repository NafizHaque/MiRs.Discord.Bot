using MediatR;
using NetCord.Services.ApplicationCommands;
using Microsoft.Extensions.DependencyInjection;

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
    }
}
