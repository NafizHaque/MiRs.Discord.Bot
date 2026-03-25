using Azure.Core;
using Azure.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MiRs.Discord.Bot.Domain.Configurations;
using MiRs.Discord.Bot.Gateway.MiRsClient;

public class MiRsTokenService : IMiRsTokenService
{
    private readonly TokenCredential _credential = new ManagedIdentityCredential();
    private readonly IOptions<AppSettings> _appsettings;
    ILogger<MiRsTokenService> _logger;

    public MiRsTokenService(IOptions<AppSettings> appsettings, ILogger<MiRsTokenService> logger)
    {
        _appsettings = appsettings;
        _logger = logger;
    }

    public async Task<string> GetTokenAsync(CancellationToken cancellationToken = default)
    {
        AccessToken token = await _credential.GetTokenAsync(
            new TokenRequestContext(new[] { _appsettings.Value.Scope }),
            cancellationToken);

        _logger.LogInformation("Token Generated: {token}", token.Token);

        return token.Token;
    }
}