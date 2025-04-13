using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MiRs.Discord.Bot.Gateway.MiRsClient;
using MiRs.Discord.Bot.Mediator;
using MiRs.Discord.Bot.Mediator.Model.Admin;

namespace MiRs.Discord.Bot.Interactors.Admin
{
    public class CreateGuildTeamInteractor : RequestHandler<CreateGuildTeamRequest, CreateGuildTeamResponse>
    {
        private readonly IMiRsAdminClient _mirsAdminClient;

        public CreateGuildTeamInteractor(IMiRsAdminClient mirsAdminClient)
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
        protected override async Task<CreateGuildTeamResponse> HandleRequest(CreateGuildTeamRequest request, CreateGuildTeamResponse result, CancellationToken cancellationToken)
        {
            await _mirsAdminClient.CreateGuildTeam(request.GuildId, request.Teamname);

            return result;
        }
    }
}
