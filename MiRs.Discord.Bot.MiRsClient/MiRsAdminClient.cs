using Flurl;
using Flurl.Http;
using MiRs.Discord.Bot.Domain.Entities;
using MiRs.Discord.Bot.Gateway.MiRsClient;

namespace MiRs.Discord.Bot.MiRsClient
{
    public class MiRsAdminClient : IMiRsAdminClient
    {
        public async Task<IEnumerable<GuildTeam>> GetGuildTeams(ulong guildId)
        {
            return await "https://localhost:7176/v1/"
                .WithHeader("Content-Type", "application/json")
                .AppendPathSegment("AdminRH/guilds")
                .SetQueryParams(new
                {
                    guildId = guildId
                })
                .GetJsonAsync<List<GuildTeam>>();
        }

        public async Task CreateGuildTeam(ulong guildId, string teamname)
        {
             await "https://localhost:7176/v1/"
                .AppendPathSegment("AdminRH/guilds")
                .SetQueryParams(new
                {
                    guildId = guildId,
                    teamname = teamname
                })
            .PostAsync();
        }
    }
}
