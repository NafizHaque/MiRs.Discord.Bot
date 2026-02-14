namespace MiRs.Discord.Bot.Domain.Entities
{
    public class EventTeamProgress
    {
        public int Id { get; set; }

        public int EventId { get; set; }

        public GuildEvent? Event { get; set; }

        public int TeamId { get; set; }

        public GuildTeam? Team { get; set; }

        public ICollection<TeamCategoryProgress>? CategoryProgresses { get; set; }
    }
}
