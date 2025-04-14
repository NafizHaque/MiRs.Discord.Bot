using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MiRs.Discord.Bot.Domain.Entities;

namespace MiRs.Discord.Bot.Mediator.Model.Admin
{
    public class GetEventsInGuildRequest : IRequest<GetEventsInGuildResponse>
    {
        public ulong GuildId { get; set; }

        public string GuildName { get; set; }
    }
}
