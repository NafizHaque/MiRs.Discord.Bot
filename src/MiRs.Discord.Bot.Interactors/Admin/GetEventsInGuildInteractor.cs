using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using MiRs.Discord.Bot.Domain.Configurations;
using MiRs.Discord.Bot.Domain.Entities;
using MiRs.Discord.Bot.Domain.Mappers;
using MiRs.Discord.Bot.Gateway.MiRsClient;
using MiRs.Discord.Bot.Mediator;
using MiRs.Discord.Bot.Mediator.Model.Admin;
using NetCord.Rest;

namespace MiRs.Discord.Bot.Interactors.Admin
{
    public class GetEventsInGuildInteractor : RequestHandler<GetEventsInGuildRequest, GetEventsInGuildResponse>
    {
        private readonly IMiRsAdminClient _mirsAdminClient;
        private readonly AppSettings _appSettings;

        public GetEventsInGuildInteractor(IMiRsAdminClient mirsAdminClient, IOptions<AppSettings> appSettings)
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
        protected override async Task<GetEventsInGuildResponse> HandleRequest(GetEventsInGuildRequest request, GetEventsInGuildResponse result, CancellationToken cancellationToken)
        {
            var guildEvents = (await _mirsAdminClient.GetGuildEvents(request.GuildId)).OrderBy(gt => gt.Id);

            StringBuilder teamidsString = new StringBuilder();
            StringBuilder teamNamesString = new StringBuilder();

            foreach (GuildEvent guildEvent in guildEvents)
            {
                teamidsString.Append($"{guildEvent.Id}\n");
                teamNamesString.Append($"{guildEvent.Eventname}\n");
            }

            teamidsString.Bold();
            teamNamesString.Bold();

            EmbedProperties embedProperties = new EmbedProperties()
                .WithTitle("**Guild Events**")
                .WithDescription("The list of all events with your server guild. ")
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
                        .WithName("Event names")
                        .WithValue(teamNamesString.ToString())
                        .WithInline()
                );

            result.GuildTeamsEmbedMessage = embedProperties;

            return result;
        }
    }
}
