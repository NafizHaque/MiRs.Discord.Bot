using MiRs.Discord.Bot.Domain.Entities;

namespace MiRs.Discord.Bot.Gateway.MiRsClient
{
    public interface IMiRsGameClient
    {
        public Task<IEnumerable<EventTeamProgress>> GetEventTeamProgress(ulong userId, ulong guildId);

        public Task<RHUserLootContainer> GetLatestTeamLoot(ulong userId, ulong guildId, ulong? channelId, ulong? messageId);
    }
}
