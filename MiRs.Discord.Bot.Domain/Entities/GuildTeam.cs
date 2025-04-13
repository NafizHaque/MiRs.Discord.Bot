using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiRs.Discord.Bot.Domain.Entities
{
    public class GuildTeam
    {
        public int Id { get; set; }

        public int GuildId { get; set; }

        public string TeamName { get; set; } = string.Empty;

    }
}
