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

        ///// <summary>
        ///// Create Event in Guild
        ///// </summary>
        //[SubSlashCommand("create", "Create Guild Event In Server!")]
        //public async Task CreateEventInGuild(
        //    [SlashCommandParameter(Name = "eventname")] string eventname,
        //    [SlashCommandParameter(Name = "start", Description = "Date in dd/mm format")] string eventstart,
        //    [SlashCommandParameter(Name = "end", Description = "Date in dd/mm format")] string eventend)
        //{
        //    try
        //    {
        //        var startDate = DateTimeOffset.ParseExact(
        //            $"{eventstart}/{DateTimeOffset.UtcNow.Year}",
        //            "dd/MM/yyyy",
        //            CultureInfo.InvariantCulture
        //        );

        //        var endDate = DateTimeOffset.ParseExact(
        //            $"{eventend}/{DateTimeOffset.UtcNow.Year}",
        //            "dd/MM/yyyy",
        //            CultureInfo.InvariantCulture
        //        );

        //        var response = await Mediator.Send(
        //            new CreateEventInGuildRequest
        //            {
        //                GuildId = Context.Guild.Id,
        //                Eventname = eventname,
        //                EventStart = startDate,
        //                EventEnd = endDate
        //            });

        //        await RespondAsync(InteractionCallback.Message(new()
        //        {
        //            Content = "Successfully created event!"
        //        }));
        //    }
        //    catch (BadRequestException ex)
        //    {
        //        await RespondAsync(InteractionCallback.Message(new()
        //        {
        //            Content = ex.Message
        //        }));
        //    }
        //    catch (Exception ex)
        //    {
        //        await RespondAsync(InteractionCallback.Message(new()
        //        {
        //            Content = $"Exception raised: {ex.Message}"
        //        }));
        //    }
        //}
    }
}
