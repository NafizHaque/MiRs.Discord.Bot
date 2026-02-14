using NetCord.Rest;

namespace MiRs.Discord.Bot.Mediator.Model.Runehunter
{
    public class GetEventsProgressResponse
    {
        public IEnumerable<IMessageComponentProperties> EventProgressComponents { get; set; }
    }
}
