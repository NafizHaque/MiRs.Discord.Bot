using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiRs.Discord.Bot.Gateway.MiRsClient;
using MiRs.Discord.Bot.Mediator.Model.Admin;
using MiRs.Discord.Bot.Mediator;
using MiRs.Discord.Bot.Domain.Entities;

namespace MiRs.Discord.Bot.Interactors.Admin
{
    public class CreateEventInGuildInteractor : RequestHandler<CreateEventInGuildRequest, CreateEventInGuildResponse>
    {
        private readonly IMiRsAdminClient _mirsAdminClient;

        public CreateEventInGuildInteractor(IMiRsAdminClient mirsAdminClient)
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
