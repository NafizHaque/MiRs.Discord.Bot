using Flurl;
using Flurl.Http;
using MiRs.Discord.Bot.Domain.Entities;
using MiRs.Discord.Bot.Gateway.MiRsClient;

namespace MiRs.Discord.Bot.MiRsClient
{
    public class MiRsUserClient : IMiRsUserClient
    {
        private readonly string _pathSegment = "RHUser";

        public async Task RegisterUser(RHUser user)
        {
            await "https://localhost:7176/v1/"
                .AppendPathSegment($"{_pathSegment}/user")
                .PostJsonAsync(user);
        }
    }
}
