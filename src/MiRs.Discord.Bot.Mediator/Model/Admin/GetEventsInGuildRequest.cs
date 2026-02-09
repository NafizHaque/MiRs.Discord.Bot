using MediatR;

namespace MiRs.Discord.Bot.Mediator.Model.Admin
{
    public class GetEventsInGuildRequest : IRequest<GetEventsInGuildResponse>
    {
        public ulong GuildId { get; set; }

        public string GuildName { get; set; } = string.Empty;
    }
}
