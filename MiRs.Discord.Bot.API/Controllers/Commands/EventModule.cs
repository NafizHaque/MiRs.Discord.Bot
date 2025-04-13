using System.Globalization;
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
        [SubSlashCommand("create", "Create Guild Event In Server!")]
        public async Task CreateEventInGuild(
            [SlashCommandParameter(Name = "eventname")] string eventname,
            [SlashCommandParameter(Name = "start", Description = "Date in dd/mm format")] string eventstart,
            [SlashCommandParameter(Name = "end", Description = "Date in dd/mm format")] string eventend)
        {
            try
            {
                var startDate = DateTimeOffset.ParseExact(
                    $"{eventstart}/{DateTimeOffset.UtcNow.Year}",
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture
                );

                var endDate = DateTimeOffset.ParseExact(
                    $"{eventend}/{DateTimeOffset.UtcNow.Year}",
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture
                );

                var response = await Mediator.Send(
                    new CreateEventInGuildRequest 
                    { 
                        GuildId = Context.Guild.Id, 
                        Eventname = eventname,
                        EventStart = startDate,
                        EventEnd = endDate
                    });


                await RespondAsync(InteractionCallback.Message(new()
                {
                    Content = "Successfully created event!"
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
