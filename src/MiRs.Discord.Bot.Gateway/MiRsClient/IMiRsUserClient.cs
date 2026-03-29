using MiRs.Discord.Bot.Domain.Entities;

namespace MiRs.Discord.Bot.Gateway.MiRsClient
{
    public interface IMiRsUserClient
    {
        public Task RegisterUser(RHUser user);

        public Task<RHUserLootContainer> GetLatestUserLoot(ulong userId);

    }
}
