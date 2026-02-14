using MediatR;

namespace MiRs.Discord.Bot.Mediator.Model.Runehunter
{
    public class GetEventsProgressRequest : IRequest<GetEventsProgressResponse>
    {
        public ulong UserId { get; set; }

        public ulong GuildId { get; set; }
    }
}
