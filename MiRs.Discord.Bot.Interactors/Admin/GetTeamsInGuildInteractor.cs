using MiRs.Discord.Bot.Mediator.Model.Home;
using MiRs.Discord.Bot.Mediator;
using MiRs.Discord.Bot.Mediator.Model.Admin;
using MiRs.Discord.Bot.Gateway.MiRsClient;

namespace MiRs.Discord.Bot.Interactors.Admin
{
    public class GetTeamsInGuildInteractor : RequestHandler<GetTeamsInGuildRequest, GetTeamsInGuildResponse>
    {
        private readonly IMiRsAdminClient _mirsAdminClient;

        public GetTeamsInGuildInteractor(IMiRsAdminClient mirsAdminClient)
        {
            _mirsAdminClient = mirsAdminClient;
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
            result.GuildTeams = await _mirsAdminClient.GetGuildTeams(request.GuildId);

            return result;
        }
    }
}
