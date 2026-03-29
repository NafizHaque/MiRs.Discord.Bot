using System.Text.Json.Serialization;

namespace MiRs.Discord.Bot.Domain.Entities
{
    public class RHTeamLootContainer
    {
        [JsonPropertyName("TeamName")]
        public string TeamName { get; set; } = string.Empty;

        [JsonPropertyName("Loots")]
        public List<RHUserLoot> TeamLoot { get; set; }

    }
}
