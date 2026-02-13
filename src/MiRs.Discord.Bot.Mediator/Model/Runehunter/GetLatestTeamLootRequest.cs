using MediatR;

namespace MiRs.Discord.Bot.Mediator.Model.Runehunter
{
    public class GetLatestTeamLootRequest : IRequest<GetLatestTeamLootResponse>
    {
        public ulong UserId { get; set; }

        public ulong GuildId { get; set; }

        public ulong ResponseId { get; set; }

        public string ResponseToken { get; set; }
    }
}
