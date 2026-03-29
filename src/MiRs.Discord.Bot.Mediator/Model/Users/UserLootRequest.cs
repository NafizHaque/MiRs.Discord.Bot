using MediatR;

namespace MiRs.Discord.Bot.Mediator.Model.Users
{
    public class UserLootRequest : IRequest<UserLootResponse>
    {
        public ulong UserId { get; set; }

    }
}
