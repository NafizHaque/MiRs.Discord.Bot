using MiRs.Discord.Bot.Domain.Entities;

namespace MiRs.Discord.Bot.Gateway.MiRsClient
{
    public interface IMiRsGameClient
    {
        public Task<IEnumerable<EventTeamProgress>> GetEventTeamProgress(ulong userId, ulong guildId);
    }
}
