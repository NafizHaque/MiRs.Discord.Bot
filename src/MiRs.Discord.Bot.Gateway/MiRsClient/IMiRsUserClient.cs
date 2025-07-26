using MiRs.Discord.Bot.Domain.Entities;

namespace MiRs.Discord.Bot.Gateway.MiRsClient
{
    public interface IMiRsUserClient
    {
        Task RegisterUser(RHUser user);

        Task JoinTeam(ulong userid, ulong guildid, string teamname);
    }
}
