namespace MiRs.Discord.Bot.Domain.Configurations
{
    public class AppSettings
    {
        public IList<ulong> DiscordSuperAdmins { get; set; }

        public IList<string> RuneHunterMonsterImages { get; set; }

        public string BaseUrl { get; set; } = string.Empty;

        public string Scope { get; set; } = string.Empty;
    }
}
