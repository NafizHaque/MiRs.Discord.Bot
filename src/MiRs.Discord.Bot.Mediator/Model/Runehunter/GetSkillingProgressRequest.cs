using MediatR;

namespace MiRs.Discord.Bot.Mediator.Model.Runehunter
{
    public class GetSkillingProgressRequest : IRequest<GetSkillingProgressResponse>
    {
        public ulong UserId { get; set; }

        public ulong GuildId { get; set; }
    }
}
