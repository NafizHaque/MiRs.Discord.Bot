using System.Text.Json.Serialization;

namespace MiRs.Discord.Bot.Domain.Entities
{
    public class RHUserLootContainer
    {
        [JsonPropertyName("Loots")]
        public List<RHUserLoot> UserLoot { get; set; }
    }
}
