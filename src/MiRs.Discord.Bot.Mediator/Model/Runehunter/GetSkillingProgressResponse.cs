using NetCord.Rest;

namespace MiRs.Discord.Bot.Mediator.Model.Runehunter
{
    public class GetSkillingProgressResponse
    {
        public IEnumerable<IMessageComponentProperties> EventProgressComponents { get; set; }

    }
}
