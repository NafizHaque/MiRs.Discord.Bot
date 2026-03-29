using Microsoft.Extensions.Options;
using MiRs.Discord.Bot.Domain.Configurations;
using MiRs.Discord.Bot.Domain.Entities;
using MiRs.Discord.Bot.Gateway.MiRsClient;
using MiRs.Discord.Bot.Mediator;
using MiRs.Discord.Bot.Mediator.Model.Users;
using NetCord;
using NetCord.Rest;
using System.Globalization;
using System.Text;

namespace MiRs.Discord.Bot.Interactors.User
{
    public class UserLootInteractor : RequestHandler<RegisterUserRequest, RegisterUserResponse>
    {
        private readonly IMiRsUserClient _mirsUserClient;
        private readonly AppSettings _appSettings;
        public UserLootInteractor(IMiRsUserClient mirsUserClient, IOptions<AppSettings> appSettings)
        {
            _mirsUserClient = mirsUserClient;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// Handles the request to get usr loot.
        /// </summary>
        /// <param name="request">The request to create user.</param>
        /// <param name="result">User object that was created.</param>
        /// <param name="cancellationToken">The cancellation token for the request.</param>
        /// <returns>Returns the user object that is created, if user is not created returns null.</returns>
        protected override async Task<RegisterUserResponse> HandleRequest(RegisterUserRequest request, RegisterUserResponse result, CancellationToken cancellationToken)
        {
            RHUserLootContainer userLoot = (await _mirsUserClient.GetLatestUserLoot(request.UserId));

            ComponentContainerProperties containerBuilder = new ComponentContainerProperties { AccentColor = new Color(0, 159, 225) };

            containerBuilder.AddComponents(new TextDisplayProperties($"## {request.UserId} Latest Loot"));

            containerBuilder.AddComponents(new ComponentSeparatorProperties().WithSpacing(ComponentSeparatorSpacingSize.Small));

            IList<string> smallImage = _appSettings.RuneHunterMonsterImages
            .OrderBy(x => new Random().Next())
            .ToList();

            StringBuilder content = new StringBuilder();

            content.AppendLine("```prolog");

            foreach (RHUserLoot loot in userLoot.UserLoot)
            {
                string username = char.ToUpper(loot.Username[0]) + loot.Username.Substring(1).ToLower();
                string lootName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(loot.Loot.ToLower());
                string quantity = loot.Quantity == 1 ? " " : $" ({loot.Quantity}) ";

                content.Append($"[{loot.DateLogged.ToString("HH:mm")}] {username} got{quantity}{lootName}\n");

                string secondLine = $"from {loot.Mobname.ToLower()}";

                content.Append($"{secondLine}\n");

                content.Append($"\n");

            }

            content.Append("```");
            containerBuilder.AddComponents(
            new ComponentSectionProperties(new ComponentSectionThumbnailProperties(new ComponentMediaProperties(smallImage[0])))
            .AddComponents(new TextDisplayProperties(content.ToString())));

            IList<IMessageComponentProperties> messageBuilder = [containerBuilder];

            result.LatestLootComponents = messageBuilder;

            return result;
        }
    }
}
