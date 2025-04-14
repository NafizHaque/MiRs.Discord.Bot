using MediatR;

namespace MiRs.Discord.Bot.Mediator.Model.Users
{
    public class JoinTeamRequest : IRequest<JoinTeamResponse>
    {
        public ulong UserId { get; set; }

        public string Teamname { get; set; } = string.Empty;
    }
}
