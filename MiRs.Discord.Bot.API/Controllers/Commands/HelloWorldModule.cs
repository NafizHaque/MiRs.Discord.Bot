using MediatR;
using NetCord;
using NetCord.Rest;
using NetCord.Services.ApplicationCommands;

namespace MiRs.Discord.Bot.API.Controllers.Commands
{
    /// <summary>
    /// Discord app command to see user Id
    /// </summary>
    public class HelloWorldModule(ISender sender) : BaseModule(sender)
    {

        /// <summary>
        /// Discord app command to see user Id
        /// </summary>
        [UserCommand("ID")]
        public string Id(User user)
        {
            return user.Id.ToString();
        }

        /// <summary>
        /// Discord app command to see message create date
        /// </summary>
        [MessageCommand("Timestamp")]
        public string Timestamp(RestMessage message)
        {
            return message.CreatedAt.ToString();
        }

        /// <summary>
        /// Simple ping pong command
        /// </summary>
        [SlashCommand("ping", "Ping!")]
        public string Pong()
        {
            return $"Ping!";
        }
    }
}
