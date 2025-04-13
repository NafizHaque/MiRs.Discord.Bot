using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiRs.Discord.Bot.Domain.Entities;

namespace MiRs.Discord.Bot.Gateway.MiRsClient
{
    public interface IMiRsAdminClient
    {
        Task<IEnumerable<GuildTeam>> GetGuildTeams(ulong guildId);

        Task CreateGuildTeam(ulong guildId, string teamname);
    }
}
