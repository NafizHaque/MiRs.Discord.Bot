using Flurl.Http;
using MiRs.Discord.Bot.Domain.Entities;
using MiRs.Discord.Bot.Gateway.MiRsClient;

namespace MiRs.Discord.Bot.MiRsClient
{
    public class MiRsGameClient : IMiRsGameClient
    {
        public async Task<IEnumerable<EventTeamProgress>> GetEventTeamProgress(ulong userId, ulong guildId)
        {
            EventTeamProgressContainer response = await "https://localhost:7176/v1/"
                .WithHeader("Content-Type", "application/json")
                .AppendPathSegment("runehunter/progress")
                .SetQueryParams(new
                {
                    userId = userId,
                    guildId = guildId
                })
                .GetJsonAsync<EventTeamProgressContainer>();

            return response.EventTeamProgresses;
        }

        public async Task<RHUserLootContainer> GetLatestTeamLoot(ulong userId, ulong guildId, ulong? responseId, string? token)
        {
            RHUserLootContainer response = await "https://localhost:7176/v1/"
                .WithHeader("Content-Type", "application/json")
                .AppendPathSegment("runehunter/loot")
                .SetQueryParams(new
                {
                    userId = userId,
                    guildId = guildId,
                    responseId = responseId,
                    token = token

                })
                .GetJsonAsync<RHUserLootContainer>();

            return response;
        }

    }
}
