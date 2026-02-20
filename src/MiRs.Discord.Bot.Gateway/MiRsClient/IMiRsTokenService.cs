namespace MiRs.Discord.Bot.Gateway.MiRsClient
{
    public interface IMiRsTokenService
    {
        public Task<string> GetTokenAsync(CancellationToken cancellationToken = default);

    }
}
