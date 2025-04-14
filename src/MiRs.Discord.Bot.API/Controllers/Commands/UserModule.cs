using MediatR;
using Microsoft.Extensions.Options;
using MiRs.Discord.Bot.Domain.Configurations;
using MiRs.Discord.Bot.Domain.Exceptions;
using MiRs.Discord.Bot.Mediator.Model.Admin;
using NetCord.Rest;
using NetCord.Services.ApplicationCommands;

namespace MiRs.Discord.Bot.API.Controllers.Commands
{
    public class UserModule(ISender sender, IOptions<AppSettings> appSettings) : BaseModule(sender, appSettings)
    {
        /// <summary>
        /// Simple ping pong command
        /// </summary>
        [SlashCommand("register", "Register yourself!")]
        public async Task RegisterUser()
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

                await RespondAsync(InteractionCallback.Message(new()
                {
                    Content = ex.Message
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
