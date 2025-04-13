using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiRs.Discord.Bot.Domain.Entities;

namespace MiRs.Discord.Bot.Mediator.Model.Admin
{
    public class GetTeamsInGuildResponse
    {
        public IEnumerable<GuildTeam> GuildTeams { get; set; } = Enumerable.Empty<GuildTeam>();
    }
}
