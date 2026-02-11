using MediatR;

namespace MiRs.Discord.Bot.Mediator.Model.Admin
{
    public class GetTeamsInGuildRequest : IRequest<GetTeamsInGuildResponse>
    {
        public ulong GuildId { get; set; }

        public string GuildName { get; set; } = string.Empty;
    }
}
