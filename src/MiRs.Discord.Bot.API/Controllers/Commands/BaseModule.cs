using MediatR;
using Microsoft.Extensions.Options;
using MiRs.Discord.Bot.Domain.Configurations;
using NetCord;
using NetCord.Services.ApplicationCommands;

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
        ///  gets the Appsetting object
        /// </summary>
        protected AppSettings Appsettings { get; }

        /// <summary>
        /// Creates MediatR object
        /// </summary>
        protected BaseModule(ISender mediator, IOptions<AppSettings> appSettings)
        {
            Mediator = mediator;
            Appsettings = appSettings.Value;
        }

        /// <summary>
        /// Check if user is validated
        /// </summary>
        public async Task<bool> UserValidated()
        {
            if (Appsettings.DiscordSuperAdmins.Contains(Context.User.Id))
            {
                return true;
            }

            if ((await Context.Client.Rest.GetGuildUserAsync(Context.Guild.Id, Context.User.Id)).GetPermissions(Context.Guild).HasFlag(Permissions.Administrator))
            {
                return true;
            }

            return await Task.FromResult(false);
        }
    }
}
