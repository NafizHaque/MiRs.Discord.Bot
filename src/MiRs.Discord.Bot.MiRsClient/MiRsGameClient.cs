using Flurl.Http;
using Microsoft.Extensions.Options;
using MiRs.Discord.Bot.Domain.Configurations;
using MiRs.Discord.Bot.Domain.Entities;
using MiRs.Discord.Bot.Gateway.MiRsClient;

namespace MiRs.Discord.Bot.MiRsClient
{
    public class MiRsGameClient : IMiRsGameClient
    {
        private readonly IOptions<AppSettings> _appsettings;
        private readonly IMiRsTokenService _miRsTokenService;

        public MiRsGameClient(IOptions<AppSettings> appsettings, IMiRsTokenService miRsTokenService)
        {
            _appsettings = appsettings;
            _miRsTokenService = miRsTokenService;
        }

        public async Task<IEnumerable<EventTeamProgress>> GetEventTeamProgress(ulong userId, ulong guildId)
        {
            string token = await _miRsTokenService.GetTokenAsync();

            EventTeamProgressContainer response = await _appsettings.Value.BaseUrl
                .WithHeader("Content-Type", "application/json")
                .WithOAuthBearerToken(token)
                .AppendPathSegment("runehunter/progress")
                .SetQueryParams(new
                {
                    userId = userId,
                    guildId = guildId
                })
                .GetJsonAsync<EventTeamProgressContainer>();

            return response.EventTeamProgresses;
        }

        public async Task<RHUserLootContainer> GetLatestTeamLoot(ulong userId, ulong guildId, ulong? channelId, ulong? messageId)
        {
            string token = await _miRsTokenService.GetTokenAsync();

            RHUserLootContainer response = await _appsettings.Value.BaseUrl
                .WithHeader("Content-Type", "application/json")
                .WithOAuthBearerToken(token)
                .AppendPathSegment("runehunter/loot")
                .SetQueryParams(new
                {
                    userId = userId,
                    guildId = guildId,
                    channelId = channelId,
                    messageId = messageId

                })
                .GetJsonAsync<RHUserLootContainer>();

            return response;
        }

    }
}
