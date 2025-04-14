using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MiRs.Discord.Bot.Domain.Configurations;
using MiRs.Discord.Bot.Domain.Entities;
using MiRs.Discord.Bot.Gateway.MiRsClient;
using MiRs.Discord.Bot.Mediator.Model.Admin;
using MiRs.Discord.Bot.Mediator;
using MiRs.Discord.Bot.Mediator.Model.Users;

namespace MiRs.Discord.Bot.Interactors.User
{
    internal class JoinTeamInteractor : RequestHandler<JoinTeamRequest, JoinTeamResponse>
    {
        private readonly IMiRsUserClient _mirsUserClient;
        private readonly AppSettings _appSettings;
        public JoinTeamInteractor(IMiRsUserClient mirsUserClient, IOptions<AppSettings> appSettings)
        {
            _mirsUserClient = mirsUserClient;
            _appSettings = appSettings.Value;
        }


        /// <summary>
        /// Handles the request to get teams in guiild.
        /// </summary>
        /// <param name="request">The request to create user.</param>
        /// <param name="result">User object that was created.</param>
        /// <param name="cancellationToken">The cancellation token for the request.</param>
        /// <returns>Returns the user object that is created, if user is not created returns null.</returns>
        protected override async Task<JoinTeamResponse> HandleRequest(JoinTeamRequest request, JoinTeamResponse result, CancellationToken cancellationToken)
        {
            await _mirsUserClient.JoinTeam(request.UserId, request.Teamname);

            return result;
        }
    }
}
