namespace MiRs.Discord.Bot.Domain.Configurations
{
    public class AppSettings
    {
        public IList<ulong> DiscordSuperAdmins { get; set; }

        public IList<string> RuneHunterMonsterImages { get; set; }

        public string ApiBaseUrl { get; set; } = string.Empty;

        public string ApiScope { get; set; } = string.Empty;
    }
}
