namespace MiRs.Discord.Bot.Domain.Entities
{
    public class TeamLevelProgress
    {
        public int Id { get; set; }

        public bool IsComplete { get; set; }

        public bool IsActive { get; set; }

        public DateTimeOffset LastUpdated { get; set; }

        public int LevelId { get; set; }

        public Level? Level { get; set; }

        public int CategoryProgressId { get; set; }

        public ICollection<TeamTaskProgress>? LevelTaskProgress { get; set; }
    }
}
