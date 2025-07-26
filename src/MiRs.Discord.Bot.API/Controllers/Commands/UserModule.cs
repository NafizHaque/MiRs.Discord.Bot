using Flurl.Http;
using MediatR;
using Microsoft.Extensions.Options;
using MiRs.Discord.Bot.Domain.Configurations;
using MiRs.Discord.Bot.Domain.Exceptions;
using MiRs.Discord.Bot.Mediator.Model.Admin;
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

                var response = await Mediator.Send(new RegisterUserRequest { UserId = Context.User.Id, Username = Context.User.Username, RunescapeUsername = runescapename });

                await RespondAsync(InteractionCallback.Message(new()
                {
                    Content = "Success Create User!"
                }));

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
                    Content = $"Exception raised: {ex.Message}"
                }));
            }
        }


        /// <summary>
        /// Create Event in Guild
        /// </summary>
        [SubSlashCommand("jointeam", "Join a Team!")]
        public async Task AddTeamToEventInGuild(
            [SlashCommandParameter(Name = "eventId")] int eventId,
            [SlashCommandParameter(Name = "teamname")] string teamname)
        {
            try
            {

                var response = await Mediator.Send(
                    new JoinTeamRequest
                    {
                        UserId = Context.User.Id,
                        EventId = eventId,
                        Teamname = teamname,
                        GuildId = Context.Guild.Id,

                    });

                await RespondAsync(InteractionCallback.Message(new()
                {
                    Content = "Successfully created event!"
                }));
            }
            catch (BadRequestException ex)
            {
                await RespondAsync(InteractionCallback.Message(new()
                {
                    Content = ex.Message
                }));
            }
            catch (Exception ex)
            {
                await RespondAsync(InteractionCallback.Message(new()
                {
                    Content = $"Exception raised: {ex.Message}"
                }));
            }
        }
    }
}
