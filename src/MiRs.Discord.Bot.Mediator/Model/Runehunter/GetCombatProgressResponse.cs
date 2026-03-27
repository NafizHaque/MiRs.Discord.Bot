using NetCord.Rest;

namespace MiRs.Discord.Bot.Mediator.Model.Runehunter
{
    public class GetCombatProgressResponse
    {
        public IEnumerable<IMessageComponentProperties> EventProgressComponents { get; set; }
    }
}
