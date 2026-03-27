using Flurl.Http;
using MediatR;
using Microsoft.Extensions.Options;
using MiRs.Discord.Bot.Domain.Configurations;
using MiRs.Discord.Bot.Mediator.Model.Users;
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
            try
            {
                await RespondAsync(InteractionCallback.DeferredMessage());

                RegisterUserResponse response = await Mediator.Send(new RegisterUserRequest { UserId = Context.User.Id, Username = Context.User.Username, RunescapeUsername = runescapename });

                await FollowupAsync(new InteractionMessageProperties()
                {
                    Content = "Successfully Created User!"
                });

            }
            catch (FlurlHttpException ex)
            {
                await RespondAsync(InteractionCallback.Message(new()
                {
                    Content = (await ex.GetResponseStringAsync()).Trim('"')
                }));
            }
            catch (Exception ex)
            {
                await RespondAsync(InteractionCallback.Message(new()
                {
                    Content = $"Could not verify registration. please try again"
                }));
            }
        }
    }
}
