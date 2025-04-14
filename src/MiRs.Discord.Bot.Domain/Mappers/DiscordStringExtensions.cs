using System.Text;

namespace MiRs.Discord.Bot.Domain.Mappers
{
    public static class DiscordStringExtensions
    {
        public static StringBuilder Bold(this StringBuilder sb)
        {
            return sb.Insert(0, "**").Append("**");
        }
    }
}
