using MediatR;
using MiRs.Discord.Bot.Domain.Exceptions;
using MiRs.Discord.Bot.Mediator.Model.Admin;
using NetCord.Rest;
using NetCord.Services.ApplicationCommands;

namespace MiRs.Discord.Bot.API.Controllers.Commands
{
    [SlashCommand("event", "Commands For RuneHunter Events!")]
    public class EventModule(ISender sender) : BaseModule(sender)
    {
        /// <summary>
        /// Get All Team in Guild
        /// </summary>
        [SubSlashCommand("get", "Return last 5 RH events in the server!")]
        public async Task GetTeamsInGuild()
        {
            try
            {
                var response = await Mediator.Send(new GetEventsInGuildRequest { GuildId = Context.Guild.Id });

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

        /// <summary>
        /// Create Event in Guild
        /// </summary>
        [SubSlashCommand("get", "Return last 5 RH events in the server!")]
        public async Task CreateEventInGuild()
        {
            try
            {
                var response = await Mediator.Send(new CreateEventInGuildRequest { GuildId = Context.Guild.Id });

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
