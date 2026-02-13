namespace MiRs.Discord.Bot.Domain.Entities
{
    public class GuildPermissions
    {
        public int Id { get; set; }

        public ulong GuildId { get; set; }

        public ulong ChannelId { get; set; }
    }
}
