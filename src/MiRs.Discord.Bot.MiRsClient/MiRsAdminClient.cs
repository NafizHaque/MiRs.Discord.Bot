using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Logging;
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
        ILogger<MiRsAdminClient> _logger;

        public MiRsAdminClient(IOptions<AppSettings> appsettings, IMiRsTokenService miRsTokenService, ILogger<MiRsAdminClient> logger)
        {
            _appsettings = appsettings;
            _miRsTokenService = miRsTokenService;
            _logger = logger;
        }

        public async Task<IEnumerable<GuildTeam>> GetGuildTeams(ulong guildId)
        {
            string token = await _miRsTokenService.GetTokenAsync();
            GuildTeamContainer response = await _appsettings.Value.BaseUrl
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
            _logger.LogInformation("THIS IS THE TOKEN!!!! {token}", token);
            GuildEventContainer response = await _appsettings.Value.BaseUrl
                .WithHeader("Content-Type", "application/json")
                .WithHeader("Authorization", $"Bearer {token}")
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

            GuildPermissions response = await _appsettings.Value.BaseUrl
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
