using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiRs.Discord.Bot.Domain.Entities;
using NetCord;
using NetCord.Rest;

namespace MiRs.Discord.Bot.Mediator.Model.Admin
{
    public class GetTeamsInGuildResponse
    {
        public EmbedProperties GuildTeamsEmbedMessage { get; set; }
    }
}
