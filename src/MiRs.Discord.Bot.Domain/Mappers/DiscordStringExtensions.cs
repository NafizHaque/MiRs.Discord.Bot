using System.Reflection.Metadata;
using System.Text;

namespace MiRs.Discord.Bot.Domain.Mappers
{
    public static class DiscordStringExtensions
    {
        public static StringBuilder Bold(this StringBuilder sb)
        {
            return sb.Insert(0, "**").Append("**");
        }

        public static StringBuilder Wrapper(this StringBuilder sb, string wrapper)
        {
            return sb.Insert(0, wrapper).Append(wrapper);
        }
    }
}
