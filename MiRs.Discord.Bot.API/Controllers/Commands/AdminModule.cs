using MediatR;
using MiRs.Discord.Bot.Domain.Exceptions;
using MiRs.Discord.Bot.Mediator.Model.Admin;
using MiRs.Discord.Bot.Mediator.Model.Home;
using NetCord.Services.ApplicationCommands;

namespace MiRs.Discord.Bot.API.Controllers.Commands
{
    public class AdminModule(ISender sender) : BaseModule(sender)
    {
        /// <summary>
        /// Get All Team in Guild
        /// </summary>
        [SlashCommand("guildteams", "Return all teams created in the server!")]
        public async Task<string> GetTeamsInGuild()
        {
            try
            {
                var teams = await Mediator.Send(new GetTeamsInGuildRequest { GuildId = Context.Guild.Id });

                return $" First team found: {teams.GuildTeams.FirstOrDefault().TeamName}";

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
        [SlashCommand("createteam", "Return all teams created in the server!")]
        public async Task<string> CreateTeamsInGuild()
        {
            try
            {
                await Mediator.Send(new CreateGuildTeamRequest { GuildId = Context.Guild.Id, Teamname = "test" });

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
