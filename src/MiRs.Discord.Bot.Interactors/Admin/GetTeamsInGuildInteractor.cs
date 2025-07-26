using MiRs.Discord.Bot.Mediator.Model.Home;
using MiRs.Discord.Bot.Mediator;
using MiRs.Discord.Bot.Mediator.Model.Admin;
using MiRs.Discord.Bot.Gateway.MiRsClient;
using NetCord.Rest;
using System.Text;
using MiRs.Discord.Bot.Domain.Entities;
using MiRs.Discord.Bot.Domain.Mappers;
using MiRs.Discord.Bot.Domain.Configurations;
using Microsoft.Extensions.Options;
using NetCord.Gateway;
using NetCord;

namespace MiRs.Discord.Bot.Interactors.Admin
{
    public class GetTeamsInGuildInteractor : RequestHandler<GetTeamsInGuildRequest, GetTeamsInGuildResponse>
    {
        private readonly IMiRsAdminClient _mirsAdminClient;
        private readonly AppSettings _appSettings;

        public GetTeamsInGuildInteractor(IMiRsAdminClient mirsAdminClient, IOptions<AppSettings> appSettings)
        {
            _mirsAdminClient = mirsAdminClient;
            _appSettings = appSettings.Value;
        }


        /// <summary>
        /// Handles the request to get teams in guiild.
        /// </summary>
        /// <param name="request">The request to create user.</param>
        /// <param name="result">User object that was created.</param>
        /// <param name="cancellationToken">The cancellation token for the request.</param>
        /// <returns>Returns the user object that is created, if user is not created returns null.</returns>
        protected override async Task<GetTeamsInGuildResponse> HandleRequest(GetTeamsInGuildRequest request, GetTeamsInGuildResponse result, CancellationToken cancellationToken)
        {
            var guildTeams = (await _mirsAdminClient.GetGuildTeams(request.GuildId)).OrderBy(ge => ge.Id);

            StringBuilder teamidsString = new StringBuilder();
            StringBuilder teamNamesString = new StringBuilder();

            foreach (GuildTeam guildTeam in guildTeams)
            {
                teamidsString.Append($"{guildTeam.Id}\n");
                teamNamesString.Append($"{guildTeam.TeamName}\n");
            }

            teamidsString.Bold();
            teamNamesString.Bold();

            EmbedProperties embedProperties = new EmbedProperties()
                .WithTitle("**Guild Teams **")
                .WithDescription("The list of all teams with your server guild. ")
                .WithTimestamp(DateTimeOffset.UtcNow)
                .WithColor(new(0x1eaae1))
                .WithAuthor(new EmbedAuthorProperties()
                    .WithName(request.GuildName))
                .AddFields(
                    new EmbedFieldProperties()
                        .WithName("Id")
                        .WithValue(teamidsString.ToString())
                        .WithInline(),
                    new EmbedFieldProperties()
                        .WithName("Team names")
                        .WithValue(teamNamesString.ToString())
                        .WithInline()
                );

            PresenceProperties presenceProperties = new PresenceProperties(UserStatusType.Online)
                .AddActivities(new UserActivityProperties("Free Hosting!", UserActivityType.Watching))
                .WithSince(DateTimeOffset.Now);
                
                

            result.GuildTeamsEmbedMessage = embedProperties;

            return result;
        }
    }
}
