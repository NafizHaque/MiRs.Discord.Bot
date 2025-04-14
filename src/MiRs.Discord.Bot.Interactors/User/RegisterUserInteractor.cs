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

namespace MiRs.Discord.Bot.Interactors.User
{
    public class RegisterUserInteractor : RequestHandler<CreateEventInGuildRequest, CreateEventInGuildResponse>
    {
        private readonly IMiRsAdminClient _mirsAdminClient;
        private readonly AppSettings _appSettings;
        public RegisterUserInteractor(IMiRsAdminClient mirsAdminClient, IOptions<AppSettings> appSettings)
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
        protected override async Task<CreateEventInGuildResponse> HandleRequest(CreateEventInGuildRequest request, CreateEventInGuildResponse result, CancellationToken cancellationToken)
        {
            GuildEvent guildEventTobeCreated = new GuildEvent
            {
                GuildId = request.GuildId,
                Eventname = request.Eventname,
                EventStart = request.EventStart,
                EventEnd = request.EventEnd,
                EventActive = true,
                CreatedDate = DateTimeOffset.Now,

            };

            await _mirsAdminClient.CreateGuildEvent(guildEventTobeCreated);

            return result;
        }
    }
}
