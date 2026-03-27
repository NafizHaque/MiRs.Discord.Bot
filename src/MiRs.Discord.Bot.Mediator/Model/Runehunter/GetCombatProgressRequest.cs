using MediatR;

namespace MiRs.Discord.Bot.Mediator.Model.Runehunter
{
    public class GetCombatProgressRequest : IRequest<GetCombatProgressResponse>
    {
        public ulong UserId { get; set; }

        public ulong GuildId { get; set; }
    }
}
