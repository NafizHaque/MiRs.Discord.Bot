namespace MiRs.Discord.Bot.Domain.Entities
{
    public class TeamTaskProgress
    {
        public int Id { get; set; }

        public int Progress { get; set; }

        public bool IsComplete { get; set; }

        public DateTimeOffset LastUpdated { get; set; }

        public int GuildEventTeamId { get; set; }

        public int CategoryLevelProcessId { get; set; }

        public int LevelTaskId { get; set; }

        public LevelTask LevelTask { get; set; }
    }
}
