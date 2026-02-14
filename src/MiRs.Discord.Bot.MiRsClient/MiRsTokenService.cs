using Azure.Core;
using Azure.Identity;
using Microsoft.Extensions.Options;
using MiRs.Discord.Bot.Domain.Configurations;
using MiRs.Discord.Bot.Gateway.MiRsClient;

public class MiRsTokenService : IMiRsTokenService
{
    private readonly TokenCredential _credential = new DefaultAzureCredential();
    private readonly IOptions<AppSettings> _appsettings;

    public MiRsTokenService(IOptions<AppSettings> appsettings)
    {
        _appsettings = appsettings;
    }

    public async Task<string> GetTokenAsync(CancellationToken cancellationToken = default)
    {
        AccessToken token = await _credential.GetTokenAsync(
            new TokenRequestContext(new[] { _appsettings.Value.ApiScope }),
            cancellationToken);

        return token.Token;
    }
}