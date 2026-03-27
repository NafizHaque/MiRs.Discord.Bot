using Azure.Core;
using Azure.Identity;
using Flurl.Http;
using MediatR;
using Microsoft.Extensions.Options;
using MiRs.Discord.Bot.Domain.Configurations;
using MiRs.Discord.Bot.Domain.Mappers;
using NetCord;
using NetCord.Gateway;
using NetCord.Rest;
using NetCord.Services.ApplicationCommands;
using System.Diagnostics;
using System.Text;

namespace MiRs.Discord.Bot.API.Controllers.Commands
{
    public class HomeModule(ISender sender, IOptions<AppSettings> appSettings) : BaseModule(sender, appSettings)
    {
        /// <summary>
        /// Discord app command to see user Id
        /// </summary>
        [UserCommand("ID")]
        public string Id(User user)
        {
            return user.Id.ToString();
        }

        /// <summary>
        /// Discord app command to see message create date
        /// </summary>
        [MessageCommand("Timestamp")]
        public string Timestamp(RestMessage message)
        {
            return message.CreatedAt.ToString();
        }

        /// <summary>
        /// Simple ping pong command
        /// </summary>
        [SlashCommand("ping", "App ping!")]
        public async Task Pong()
        {
            TokenCredential _credential = new DefaultAzureCredential();

            AccessToken token = await _credential.GetTokenAsync(
                new TokenRequestContext(new[] { Appsettings.Scope }),
                CancellationToken.None);

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            await RespondAsync(InteractionCallback.DeferredMessage());

            StringBuilder sb = new StringBuilder();

            sb.Append("Doctor Mudkip....Online\n");

            try
            {
                IFlurlResponse response = await Appsettings.BaseUrl
                    .WithOAuthBearerToken(token.Token)
                   .AppendPathSegment($"Generics/ping")
                   .WithTimeout(TimeSpan.FromSeconds(10))
                   .PostAsync();
                sb.Append("MiRs Api....Online\n");

            }
            catch (Exception ex)
            {

                sb.Append("MiRs Api....Failed\n");
            }

            sb.Append($"ConnectionTime: {stopwatch.Elapsed.Milliseconds}ms");

            sb.Wrapper("```");

            stopwatch.Stop();

            InteractionMessageProperties message = new InteractionMessageProperties()
            {
                Content = sb.ToString(),
            };

            GatewayClientConfiguration x = new GatewayClientConfiguration()
            {
                Presence = new PresenceProperties(UserStatusType.DoNotDisturb)
            };

            await FollowupAsync(message);
        }
    }
}
