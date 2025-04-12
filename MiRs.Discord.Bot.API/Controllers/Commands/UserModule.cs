using NetCord.Services.ApplicationCommands;

namespace MiRs.Discord.Bot.API.Controllers.Commands
{
    public class UserModule
    {
        /// <summary>
        /// Simple ping pong command
        /// </summary>
        [SlashCommand("register", "Overall status of team matrix!")]
        public string HomeMenu()
        {
            return $"This is the hub home menu!";
        }
    }
}
