using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;
using MiRs.Discord.Bot.Domain.Configurations;
using MiRs.Discord.Bot.Domain.Entities;
using MiRs.Discord.Bot.Gateway.MiRsClient;

namespace MiRs.Discord.Bot.MiRsClient
{
    public class MiRsUserClient : IMiRsUserClient
    {
        private readonly IOptions<AppSettings> _appsettings;
        private readonly IMiRsTokenService _miRsTokenService;

        public MiRsUserClient(IOptions<AppSettings> appsettings, IMiRsTokenService miRsTokenService)
        {
            _appsettings = appsettings;
            _miRsTokenService = miRsTokenService;
        }

        public async Task RegisterUser(RHUser user)
        {
            string token = await _miRsTokenService.GetTokenAsync();

            await _appsettings.Value.BaseUrl
                .WithOAuthBearerToken(token)
                .AppendPathSegment($"user")
                .PostJsonAsync(user);
        }
    }
}
