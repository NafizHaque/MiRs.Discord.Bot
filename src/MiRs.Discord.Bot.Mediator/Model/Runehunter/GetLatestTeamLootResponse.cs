using NetCord.Rest;

namespace MiRs.Discord.Bot.Mediator.Model.Runehunter
{
    public class GetLatestTeamLootResponse
    {
        public IEnumerable<IMessageComponentProperties> LatestLootComponents { get; set; }
    }
}
