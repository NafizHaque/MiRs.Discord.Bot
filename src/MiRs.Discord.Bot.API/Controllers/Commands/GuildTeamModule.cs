using MediatR;
using Microsoft.Extensions.Options;
using MiRs.Discord.Bot.Domain.Configurations;
using MiRs.Discord.Bot.Domain.Exceptions;
using MiRs.Discord.Bot.Mediator.Model.Admin;
using NetCord.Rest;
using NetCord.Services.ApplicationCommands;

namespace MiRs.Discord.Bot.API.Controllers.Commands
{
    [SlashCommand("team", "Commands For Guild Teams!")]
    public class GuildTeamModule(ISender sender, IOptions<AppSettings> appSettings) : BaseModule(sender, appSettings)
    {
        /// <summary>
        /// Get All Team in Guild
        /// </summary>
        [SubSlashCommand("display", "Return all teams in the server!")]
        public async Task GetTeamsInGuild()
        {
            try
            {
                if (!(await UserValidated()))
                {
                    await RespondAsync(InteractionCallback.Message(new()
                    {
                        Content = $"Lack Permissions!"
                    }));

                    return;
                }
                GetTeamsInGuildResponse response = await Mediator.Send(new GetTeamsInGuildRequest { GuildId = Context.Guild.Id });

                await RespondAsync(InteractionCallback.Message(new()
                {
                    Embeds = new List<EmbedProperties> { response.GuildTeamsEmbedMessage },
                }));
            }
            catch (BadRequestException ex)
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }
    }
}
