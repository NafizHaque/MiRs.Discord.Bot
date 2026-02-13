using MiRs.Discord.Bot.Domain.Entities;

namespace MiRs.Discord.Bot.Gateway.MiRsClient
{
    public interface IMiRsAdminClient
    {
        public Task<IEnumerable<GuildTeam>> GetGuildTeams(ulong guildId);

        public Task<IEnumerable<GuildEvent>> GetGuildEvents(ulong guildId);

        public Task<GuildPermissions> GetGuildMessagePermissions(ulong guildId);
    }
}
