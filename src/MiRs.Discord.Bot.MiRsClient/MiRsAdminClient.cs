using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;
using MiRs.Discord.Bot.Domain.Configurations;
using MiRs.Discord.Bot.Domain.Entities;
using MiRs.Discord.Bot.Gateway.MiRsClient;

namespace MiRs.Discord.Bot.MiRsClient
{
    public class MiRsAdminClient : IMiRsAdminClient
    {
        private readonly IOptions<AppSettings> _appsettings;
        private readonly IMiRsTokenService _miRsTokenService;

        public MiRsAdminClient(IOptions<AppSettings> appsettings, IMiRsTokenService miRsTokenService)
        {
            _appsettings = appsettings;
            _miRsTokenService = miRsTokenService;
        }

        public async Task<IEnumerable<GuildTeam>> GetGuildTeams(ulong guildId)
        {
            string token = await _miRsTokenService.GetTokenAsync();

            GuildTeamContainer response = await _appsettings.Value.ApiBaseUrl
                .WithHeader("Content-Type", "application/json")
                .WithOAuthBearerToken(token)
                .AppendPathSegment("AdminRH/guilds")
                .SetQueryParams(new
                {
                    guildId = guildId
                })
                .GetJsonAsync<GuildTeamContainer>();

            return response.GuildTeams;
        }

        public async Task<IEnumerable<GuildEvent>> GetGuildEvents(ulong guildId)
        {
            string token = await _miRsTokenService.GetTokenAsync();

            GuildEventContainer response = await _appsettings.Value.ApiBaseUrl
                .WithHeader("Content-Type", "application/json")
                .WithOAuthBearerToken(token)
                .AppendPathSegment("events")
                .SetQueryParams(new
                {
                    guildId = guildId
                })
                .GetJsonAsync<GuildEventContainer>();

            return response.GuildEvents;
        }

        public async Task<GuildPermissions> GetGuildMessagePermissions(ulong guildId)
        {
            string token = await _miRsTokenService.GetTokenAsync();

            GuildPermissions response = await _appsettings.Value.ApiBaseUrl
                .WithHeader("Content-Type", "application/json")
                .WithOAuthBearerToken(token)
                .AppendPathSegment("Gen/guildperms")
                .SetQueryParams(new
                {
                    guildId = guildId
                })
                .GetJsonAsync<GuildPermissions>();

            return response;
        }
    }
}
