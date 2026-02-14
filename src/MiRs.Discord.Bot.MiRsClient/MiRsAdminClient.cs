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
            GuildTeamContainer response = await "https://localhost:7176/v1/"
                .WithHeader("Content-Type", "application/json")
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

        public async Task<GuildPermissions> GetGuildMessagePermissions(ulong guildId)
        {
            GuildPermissions response = await "https://localhost:7176/v1/"
                .WithHeader("Content-Type", "application/json")
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
