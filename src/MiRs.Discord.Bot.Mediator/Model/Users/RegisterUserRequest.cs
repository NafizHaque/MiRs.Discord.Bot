using MediatR;

namespace MiRs.Discord.Bot.Mediator.Model.Users
{
    public class RegisterUserRequest : IRequest<RegisterUserResponse>
    {
        public ulong UserId { get; set; }

        public string Username { get; set; } = string.Empty;

        public string RunescapeUsername { get; set; } = string.Empty;
    }
}
