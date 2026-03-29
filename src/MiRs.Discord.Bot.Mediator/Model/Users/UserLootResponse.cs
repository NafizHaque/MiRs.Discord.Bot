using NetCord.Rest;

namespace MiRs.Discord.Bot.Mediator.Model.Users
{
    public class UserLootResponse
    {
        public IEnumerable<IMessageComponentProperties> LatestLootComponents { get; set; }

    }
}
