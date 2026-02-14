namespace MiRs.Discord.Bot.Domain.Entities
{
    public class GuildEvent
    {
        public int Id { get; set; }

        public ulong GuildId { get; set; }

        public string Eventname { get; set; } = string.Empty;

        public bool EventActive { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset EventStart { get; set; }

        public DateTimeOffset EventEnd { get; set; }
    }
}
