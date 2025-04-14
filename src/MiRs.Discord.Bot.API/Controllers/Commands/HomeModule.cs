using MediatR;
using NetCord.Services.ApplicationCommands;
using Microsoft.Extensions.Options;
using MiRs.Discord.Bot.Domain.Configurations;

namespace MiRs.Discord.Bot.API.Controllers.Commands
{
    [SlashCommand("hub", "Hub for your team!")]
    public class HomeModule(ISender sender, IOptions<AppSettings> appSettings) : BaseModule(sender, appSettings)
    {
        /// <summary>
        /// Simple ping pong command
        /// </summary>
        [SubSlashCommand("home", "Overall status of team matrix!")]
        public async Task<string> HomeMenu()
        {
            //var v = await Context.User.GetDMChannelAsync();
            //await v.SendMessageAsync("test");
            //try
            //{
            //    var response = await Mediator.Send(new HomeRequest { Context = Context });
            //    throw new NotImplementedException();
            //}
            //catch (BadRequestException ex)
            //{
            //    throw new NotImplementedException();
            //}
            //catch (Exception ex)
            //{
            //    throw new NotImplementedException();
            //}

            return "Not available!";
        }

        /// <summary>
        /// Simple ping pong command
        /// </summary>
        [SubSlashCommand("armoury", "Overall status of team matrix!")]
        public string ArmouryMenu()
        {
            return $"This is the hub armoury menu!";
        }
    }
}
