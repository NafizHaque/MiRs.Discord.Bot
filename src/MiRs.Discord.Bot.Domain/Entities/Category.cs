namespace MiRs.Discord.Bot.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }

        public string name { get; set; } = string.Empty;

        public ICollection<Level>? Level { get; set; }
    }
}
