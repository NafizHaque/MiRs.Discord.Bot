using Flurl.Http;
using MediatR;
using Microsoft.Extensions.Options;
using MiRs.Discord.Bot.Domain.Configurations;
using MiRs.Discord.Bot.Domain.Exceptions;
using MiRs.Discord.Bot.Mediator.Model.Users;
using NetCord;
using NetCord.Rest;
using NetCord.Services.ApplicationCommands;

namespace MiRs.Discord.Bot.API.Controllers.Commands
{

    public class UserModule(ISender sender, IOptions<AppSettings> appSettings) : BaseModule(sender, appSettings)
    {
        /// <summary>
        /// Register User To RuneHunter.
        /// </summary>
        [SlashCommand("register", "Register to RuneHunter!")]
        public async Task RegisterUser([SlashCommandParameter(Name = "rsn")] string runescapename)
        {
            await RespondAsync(InteractionCallback.DeferredMessage());

            try
            {
                RegisterUserResponse response = await Mediator.Send(new RegisterUserRequest { UserId = Context.User.Id, Username = Context.User.Username, RunescapeUsername = runescapename });

                await FollowupAsync(new InteractionMessageProperties()
                {
                    Content = "Successfully Created User!"
                });

            }
            catch (FlurlHttpException ex)
            {
                await FollowupAsync(new InteractionMessageProperties()
                {
                    Content = (await ex.GetResponseStringAsync()).Trim('"')
                });
            }
            catch (Exception ex)
            {
                await FollowupAsync(new InteractionMessageProperties()
                {
                    Content = $"Could not verify registration. please try again | {ex.Message}"
                });
            }
        }

        /// <summary>
        /// Register User To RuneHunter.
        /// </summary>
        [SlashCommand("myloot", "Get your user loot!")]
        public async Task UserLoot()
        {
            InteractionCallbackResponse msg = await RespondAsync(InteractionCallback.DeferredMessage());

            IList<IMessageComponentProperties> messageBuilder = [new ComponentContainerProperties().AddComponents(new TextDisplayProperties($"Loading Loot Data..."))];

            RestMessage message = await FollowupAsync(new InteractionMessageProperties()
            {
                Components = messageBuilder,
                Flags = MessageFlags.IsComponentsV2 | MessageFlags.Ephemeral,
            });

            try
            {
                UserLootResponse response = await Mediator.Send(new UserLootRequest { UserId = Context.User.Id });

                EmbedProperties embedProperties = new EmbedProperties()
               .WithColor(new(0x1eaae1));

                await ModifyFollowupAsync(message.Id, message => message.Components = response.LatestLootComponents);

            }
            catch (BadRequestException ex)
            {
                IList<IMessageComponentProperties> exComponent = [new ComponentContainerProperties().AddComponents(new TextDisplayProperties($"{ex.Message}"))];

                await ModifyFollowupAsync(message.Id, message => message.Components = exComponent);

            }
            catch (FlurlHttpException ex)
            {
                await FollowupAsync(new InteractionMessageProperties()
                {
                    Content = (await ex.GetResponseStringAsync()).Trim('"')
                });
            }
            catch (Exception ex)
            {
                IList<IMessageComponentProperties> exComponent = [new ComponentContainerProperties().AddComponents(new TextDisplayProperties($"{ex.Message}"))];

                await ModifyFollowupAsync(message.Id, message => message.Components = exComponent);
            }
        }
    }
}
