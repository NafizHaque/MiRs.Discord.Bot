using MediatR;
using Microsoft.Extensions.Options;
using MiRs.Discord.Bot.Domain.Configurations;
using MiRs.Discord.Bot.Domain.Exceptions;
using MiRs.Discord.Bot.Mediator.Model.Admin;
using NetCord.Rest;
using NetCord.Services.ApplicationCommands;

namespace MiRs.Discord.Bot.API.Controllers.Commands
{
    [SlashCommand("event", "Commands For RuneHunter Events!")]
    public class EventModule(ISender sender, IOptions<AppSettings> appSettings) : BaseModule(sender, appSettings)
    {

        /// <summary>
        /// Get All Team in Guild
        /// </summary>
        [SubSlashCommand("display", "Return last 5 RH events in the server!")]
        public async Task GetEventsInGuild()
        {
            try
            {
                GetEventsInGuildResponse response = await Mediator.Send(new GetEventsInGuildRequest { GuildId = Context.Guild.Id });

                await RespondAsync(InteractionCallback.Message(new()
                {
                    Embeds = new List<EmbedProperties> { response.GuildTeamsEmbedMessage },
                }));

            }
            catch (BadRequestException ex)
            {

                await RespondAsync(InteractionCallback.Message(new()
                {
                    Content = ex.CustomErrorMessage
                }));
            }
            catch (Exception ex)
            {
                await RespondAsync(InteractionCallback.Message(new()
                {
                    Content = $"Exception raised: {ex.Message}"
                }));
            }
        }
    }
}
