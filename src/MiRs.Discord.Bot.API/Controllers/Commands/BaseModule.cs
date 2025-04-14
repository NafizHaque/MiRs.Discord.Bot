using MediatR;
using NetCord.Services.ApplicationCommands;
using NetCord.Rest;
using NetCord;
using MiRs.Discord.Bot.Domain.Configurations;
using Microsoft.Extensions.Options;

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
        protected AppSettings Appsettings {  get; }

        /// <summary>
        /// Creates MediatR object
        /// </summary>
        protected BaseModule(ISender mediator, IOptions<AppSettings> appSettings)
        {
            Mediator = mediator;
            Appsettings = appSettings.Value;
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

        /// <summary>
        /// Check if user is validated
        /// </summary>
        public async Task<bool> UserValidated()
        {
            if (Appsettings.DiscordSuperAdmins.Contains(Context.User.Id))
            {
                return true;
            }

           if((await Context.Client.Rest.GetGuildUserAsync(Context.Guild.Id, Context.User.Id)).GetPermissions(Context.Guild).HasFlag(Permissions.Administrator))
            {
                return true;
            }


            return await Task.FromResult(false);
        }
    }
}
