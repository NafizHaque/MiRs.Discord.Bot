using Microsoft.Extensions.Options;
using MiRs.Discord.Bot.Domain.Configurations;
using MiRs.Discord.Bot.Domain.Entities;
using MiRs.Discord.Bot.Gateway.MiRsClient;
using MiRs.Discord.Bot.Mediator;
using MiRs.Discord.Bot.Mediator.Model.Runehunter;
using NetCord;
using NetCord.Rest;
using System.Text;

namespace MiRs.Discord.Bot.Interactors.Runehunter
{
    public class GetCombatProgressInteractor : RequestHandler<GetCombatProgressRequest, GetCombatProgressResponse>
    {
        private readonly IMiRsGameClient _mirsGameClient;
        private readonly AppSettings _appSettings;

        public GetCombatProgressInteractor(IMiRsGameClient mirsGameClient, IOptions<AppSettings> appSettings)
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
        protected override async Task<GetCombatProgressResponse> HandleRequest(GetCombatProgressRequest request, GetCombatProgressResponse result, CancellationToken cancellationToken)
        {
            IEnumerable<EventTeamProgress> eventTeamProgress = (await _mirsGameClient.GetEventTeamProgress(request.UserId, request.GuildId)).ToList();

            ComponentContainerProperties containerBuilder = new ComponentContainerProperties { AccentColor = new Color(0, 159, 225) }.AddComponents(new MediaGalleryProperties().AddItems(new MediaGalleryItemProperties(new ComponentMediaProperties("https://files.catbox.moe/m0tkim.png"))));

            containerBuilder.AddComponents(new TextDisplayProperties($"## {eventTeamProgress.FirstOrDefault().Team.TeamName} - {DateTime.UtcNow.ToString("hh:mm:ss tt")} UTC"));

            containerBuilder.AddComponents(new ComponentSeparatorProperties().WithSpacing(ComponentSeparatorSpacingSize.Small));

            IList<string> smallImage = _appSettings.RuneHunterMonsterImages
               .OrderBy(x => new Random().Next())
               .ToList();

            int imageIndexer = 0;

            IEnumerable<string> tempCategoryCheck = new List<string>
            {"Crafting Guild", "Farming Guild", "Herbalist Guild", "Mining Guild", "Runecraft Guild", "Woodcutting Guild" };

            IList<string> tempArmouryCheck = new List<string>
            {"Lunar Chest Unlocked", "Fortis Colosseum Unlocked", "Tombs of Amascut Unlocked", "Chambers of Xeric Unlocked", "Theatre of Blood Unlocked" };

            foreach (TeamCategoryProgress prog in eventTeamProgress.FirstOrDefault().CategoryProgresses)
            {

                // Temp to split the progress ui
                if (tempCategoryCheck.Contains(prog.Category.name))
                {
                    continue;
                }

                StringBuilder content = new StringBuilder();

                content.Append($"## {prog.Category.name}\n");

                IEnumerable<TeamLevelProgress> Levels = prog.CategoryLevelProcess;

                TeamLevelProgress currentLevel = Levels
                    .Where(lp => !lp.IsActive)
                    .OrderBy(lp => lp.Level.Levelnumber)
                    .FirstOrDefault();

                if (currentLevel is null)
                {
                    currentLevel = Levels
                        .OrderByDescending(lp => lp.Level.Levelnumber)
                        .FirstOrDefault();
                }

                if (currentLevel is null)
                {
                    continue;
                }

                content.Append($"### Level: {currentLevel.Level.Levelnumber}\n");

                foreach (TeamTaskProgress task in currentLevel.LevelTaskProgress)
                {
                    content.Append($"► {task.LevelTask.Name} **{task.Progress} / {task.LevelTask.Goal}**\n");
                }

                if (string.Equals(prog.Category.name, "hub base", StringComparison.OrdinalIgnoreCase))
                {
                    content.Append($"```diff\nwhat you have currently unlocked:\n+ Hub Base level {currentLevel.Level.Levelnumber - 1} out of 9!\n```\n");
                }
                else if (string.Equals(prog.Category.name, "Training Area", StringComparison.OrdinalIgnoreCase) && currentLevel.Level.Levelnumber == 1)
                {
                    content.Append($"```diff\nwhat you have currently unlocked:\n+ Enemies under combat level 200 Unlocked!\n```\n");
                }
                else if (currentLevel.Level.Levelnumber == 1)
                {
                    content.Append($"```diff\nNothing Unlocked Yet!```\n");
                }
                else if (string.Equals(prog.Category.name, "Armoury", StringComparison.OrdinalIgnoreCase))
                {
                    bool limitReached = false;
                    int indexer = 0;
                    content.Append($"```diff\nwhat you have currently unlocked:\n```\n");
                    while (limitReached == false)
                    {

                        if (string.Equals(currentLevel.Level.UnlockDescription, tempArmouryCheck[indexer], StringComparison.CurrentCultureIgnoreCase))
                        {
                            limitReached = true;
                        }

                        if (limitReached)
                        {
                            content.Append($"```+ {tempArmouryCheck[indexer]}\n```\n");
                        }


                        indexer++;
                    }
                    content.Append($"```\n");
                }
                else if (currentLevel.Level.Levelnumber == 9 && currentLevel.IsActive)
                {
                    content.Append($"```diff\nwhat you have currently unlocked:\n+ {currentLevel.Level.UnlockDescription}\n```\n");
                }
                else
                {
                    string previousUnlock = Levels.Where(l => l.Level.Levelnumber == (currentLevel.Level.Levelnumber - 1)).Select(l => l.Level.UnlockDescription).FirstOrDefault();
                    content.Append($"```diff\nwhat you have currently unlocked:\n+{previousUnlock}\n```\n");

                }

                containerBuilder.AddComponents(
                    new ComponentSectionProperties(new ComponentSectionThumbnailProperties(new ComponentMediaProperties(smallImage[imageIndexer])))
                    .AddComponents(new TextDisplayProperties(content.ToString())));

                containerBuilder.AddComponents(
                    new ComponentSeparatorProperties().WithSpacing(ComponentSeparatorSpacingSize.Small));

                imageIndexer++;
            }

            IList<IMessageComponentProperties> messageBuilder = [
                containerBuilder
                ];

            result.EventProgressComponents = messageBuilder;
            return result;
        }
    }
}
