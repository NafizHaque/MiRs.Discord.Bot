using System.Text;
using MediatR;
using MiRs.Discord.Bot.Domain.Exceptions;
using MiRs.Discord.Bot.Mediator.Model.Admin;
using MiRs.Discord.Bot.Mediator.Model.Home;
using NetCord;
using NetCord.Gateway;
using NetCord.Rest;
using NetCord.Services.ApplicationCommands;

namespace MiRs.Discord.Bot.API.Controllers.Commands
{
    [SlashCommand("guild", "Commands For Guild Teams!")]
    public class GuildTeamModule(ISender sender) : BaseModule(sender)
    {
        /// <summary>
        /// Get All Team in Guild
        /// </summary>
        [SubSlashCommand("get", "Return all teams created in the server!")]
        public async Task GetTeamsInGuild()
        {
            try
            {
                var response = await Mediator.Send(new GetTeamsInGuildRequest { GuildId = Context.Guild.Id });

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
        /// Get All Team in Guild
        /// </summary>
        [SubSlashCommand("create", "Create team in the server!")]
        public async Task<string> CreateTeamsInGuild(
            [SlashCommandParameter(Name = "teamname")] string teamname)
        {
            try
            {
                await Mediator.Send(new CreateGuildTeamRequest { GuildId = Context.Guild.Id, Teamname = teamname });

                return $"Created Team!";

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
