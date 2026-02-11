namespace MiRs.Discord.Bot.Domain.Entities
{
    public class TeamCategoryProgress
    {
        public int Id { get; set; }

        public bool IsComplete { get; set; }

        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        public int GuildEventTeamId { get; set; }

        public ICollection<TeamLevelProgress>? CategoryLevelProcess { get; set; }
    }
}
