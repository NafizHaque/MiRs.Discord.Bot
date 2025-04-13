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
            GuildTeamContainer response =  await "https://localhost:7176/v1/"
                .WithHeader("Content-Type", "application/json")
                .AppendPathSegment("AdminRH/guilds")
                .SetQueryParams(new
                {
                    guildId = guildId
                })
                .GetJsonAsync<GuildTeamContainer>();

            return response.GuildTeams;
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

        public async Task<IEnumerable<GuildEvent>> GetGuildEvents(ulong guildId)
        {
            GuildEventContainer response = await "https://localhost:7176/v1/"
                .WithHeader("Content-Type", "application/json")
                .AppendPathSegment("AdminRH/events")
                .SetQueryParams(new
                {
                    guildId = guildId
                })
                .GetJsonAsync<GuildEventContainer>();

            return response.GuildEvents;
        }

        public async Task CreateGuildEvent(GuildEvent guildEvent)
        {
            await "https://localhost:7176/v1/"
            .AppendPathSegment("AdminRH/events")
            .PostJsonAsync(guildEvent);
        }
    }
}
