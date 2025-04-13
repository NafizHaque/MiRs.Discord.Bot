using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MiRs.Discord.Bot.Domain.Entities
{
    public class GuildTeamContainer
    {
        public List<GuildTeam> GuildTeams { get; set; }
    }
}
