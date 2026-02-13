using MediatR;
using Microsoft.Extensions.Options;
using MiRs.Discord.Bot.Domain.Configurations;
using MiRs.Discord.Bot.Domain.Exceptions;
using MiRs.Discord.Bot.Mediator.Model.Runehunter;
using NetCord;
using NetCord.Rest;
using NetCord.Services.ApplicationCommands;

namespace MiRs.Discord.Bot.API.Controllers.Commands
{
    [SlashCommand("hub", "Hub for your team!")]
    public class HomeModule(ISender sender, IOptions<AppSettings> appSettings) : BaseModule(sender, appSettings)
    {
        /// <summary>
        /// Get all current user events progress
        /// </summary>
        [SubSlashCommand("progress", "Return the current user events progress")]
        public async Task GetEventsProgress()
        {
            try
            {
                GetEventsProgressResponse response = await Mediator.Send(new GetEventsProgressRequest { UserId = Context.User.Id, GuildId = Context.Guild.Id });

                EmbedProperties embedProperties = new EmbedProperties()
               .WithColor(new(0x1eaae1));

                await RespondAsync(InteractionCallback.Message(new InteractionMessageProperties()
                {
                    Components = response.EventProgressComponents,
                    Flags = MessageFlags.IsComponentsV2 | MessageFlags.Ephemeral,
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

        /// <summary>
        /// Get latest Team Loot 
        /// </summary>
        [SubSlashCommand("drops", "Return the latest team drops")]
        public async Task GetLatestTeamLoot()
        {
            try
            {
                GetLatestTeamLootResponse response = await Mediator.Send(new GetLatestTeamLootRequest { UserId = Context.User.Id, GuildId = Context.Guild.Id, ResponseId = Context.Interaction.ApplicationId, ResponseToken = Context.Interaction.Token });

                EmbedProperties embedProperties = new EmbedProperties()
               .WithColor(new(0x1eaae1));

                await RespondAsync(InteractionCallback.Message(new InteractionMessageProperties()
                {
                    Components = response.LatestLootComponents,
                    Flags = MessageFlags.IsComponentsV2,
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
