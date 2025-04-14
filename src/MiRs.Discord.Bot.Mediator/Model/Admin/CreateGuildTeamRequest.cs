using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace MiRs.Discord.Bot.Mediator.Model.Admin
{
    public class CreateGuildTeamRequest : IRequest<CreateGuildTeamResponse>
    {
        public ulong GuildId { get; set; }

        public string Teamname { get; set; } = string.Empty;
    }
}
