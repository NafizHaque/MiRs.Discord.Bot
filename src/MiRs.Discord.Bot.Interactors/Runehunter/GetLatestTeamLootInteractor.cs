using Microsoft.Extensions.Options;
using MiRs.Discord.Bot.Domain.Configurations;
using MiRs.Discord.Bot.Domain.Entities;
using MiRs.Discord.Bot.Gateway.MiRsClient;
using MiRs.Discord.Bot.Mediator;
using MiRs.Discord.Bot.Mediator.Model.Runehunter;
using NetCord;
using NetCord.Rest;
using System.Globalization;
using System.Text;

namespace MiRs.Discord.Bot.Interactors.Runehunter
{
    public class GetLatestTeamLootInteractor : RequestHandler<GetLatestTeamLootRequest, GetLatestTeamLootResponse>
    {
        private readonly IMiRsGameClient _mirsGameClient;
        private readonly AppSettings _appSettings;

        public GetLatestTeamLootInteractor(IMiRsGameClient mirsGameClient, IOptions<AppSettings> appSettings)
        {
            _mirsGameClient = mirsGameClient;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Handles the request to get teams in guiild.
        /// </summary>
        /// <param name="request">The request to create user.</param>
        /// <param name="result">User object that was created.</param>
        /// <param name="cancellationToken">The cancellation token for the request.</param>
        /// <returns>Returns the user object that is created, if user is not created returns null.</returns>
        protected override async Task<GetLatestTeamLootResponse> HandleRequest(GetLatestTeamLootRequest request, GetLatestTeamLootResponse result, CancellationToken cancellationToken)
        {
            RHUserLootContainer teamLoot = (await _mirsGameClient.GetLatestTeamLoot(request.UserId, request.GuildId, request.ResponseId, request.ResponseToken));

            ComponentContainerProperties containerBuilder = new ComponentContainerProperties { AccentColor = new Color(0, 159, 225) }.AddComponents(new MediaGalleryProperties().AddItems(new MediaGalleryItemProperties(new ComponentMediaProperties("https://files.catbox.moe/m0tkim.png"))));

            containerBuilder.AddComponents(new TextDisplayProperties($"## {teamLoot.TeamName} Latest Loot"));

            containerBuilder.AddComponents(new ComponentSeparatorProperties().WithSpacing(ComponentSeparatorSpacingSize.Small));

            IList<string> smallImage = _appSettings.RuneHunterMonsterImages
            .OrderBy(x => new Random().Next())
            .ToList();

            StringBuilder content = new StringBuilder();

            content.AppendLine("```prolog");

            foreach (RHUserLoot loot in teamLoot.TeamLoot)
            {
                string username = char.ToUpper(loot.Username[0]) + loot.Username.Substring(1).ToLower();
                string lootName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(loot.Loot.ToLower());
                string quantity = loot.Quantity == 1 ? string.Empty : $" ({loot.Quantity}) ";

                string lootMessage = $"[{loot.DateLogged.ToString("HH:mm")}] {username} - {lootName}{quantity}from {loot.Mobname}";

                if (lootMessage.Length < 53)
                {
                    content.Append($"{lootMessage}\n");
                }
                else
                {
                    content.Append($"[{loot.DateLogged.ToString("HH:mm")}] {username} - {lootName}{quantity}\n");

                    int padLeft = $"[{loot.DateLogged.ToString("HH:mm")}] {username}- ".Length;

                    string secondLine = $"from {loot.Mobname}";

                    content.Append($"{secondLine.PadLeft(padLeft)}");
                }
                content.Append("```");
            }

            containerBuilder.AddComponents(
            new ComponentSectionProperties(new ComponentSectionThumbnailProperties(new ComponentMediaProperties(smallImage[0])))
            .AddComponents(new TextDisplayProperties(content.ToString())));

            IList<IMessageComponentProperties> messageBuilder = [containerBuilder];

            result.LatestLootComponents = messageBuilder;

            return result;
        }
    }
}
