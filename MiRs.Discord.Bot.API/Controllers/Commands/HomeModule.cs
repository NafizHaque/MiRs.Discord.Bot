using System.Net;
using MediatR;
using MiRs.Discord.Bot.Domain.Exceptions;
using NetCord;
using NetCord.Services.ApplicationCommands;
using MiRs.Discord.Bot.Mediator.Model.Home;

namespace MiRs.Discord.Bot.API.Controllers.Commands
{
    [SlashCommand("hub", "Hub for your team!")]
    public class HomeModule(ISender sender) : BaseModule(sender)
    {
        /// <summary>
        /// Simple ping pong command
        /// </summary>
        [SubSlashCommand("home", "Overall status of team matrix!")]
        public async Task<Embed> HomeMenu()
        {
            var v = await Context.User.GetDMChannelAsync();
            await v.SendMessageAsync("test");
            try
            {
                var response = await Mediator.Send(new HomeRequest { Context = Context });

            }
            catch (BadRequestException ex)
            {
                throw new NotImplementedException();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
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
