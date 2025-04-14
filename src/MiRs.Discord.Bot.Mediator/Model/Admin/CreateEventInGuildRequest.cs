using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace MiRs.Discord.Bot.Mediator.Model.Admin
{
    public class CreateEventInGuildRequest : IRequest<CreateEventInGuildResponse>
    {
        public ulong GuildId { get; set; }

        public string Eventname { get; set; } = string.Empty;

        public DateTimeOffset EventStart { get; set; }

        public DateTimeOffset EventEnd { get; set; }
    }
}
