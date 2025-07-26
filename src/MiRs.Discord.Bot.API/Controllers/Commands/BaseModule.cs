﻿using MediatR;
using NetCord.Services.ApplicationCommands;
using NetCord.Rest;
using NetCord;
using MiRs.Discord.Bot.Domain.Configurations;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using Flurl.Http;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using NetCord.Services;
using Flurl;
using NetCord.Gateway;
using NetCord.Gateway.JsonModels;
using NetCord.Services.ComponentInteractions;
using System.Text;
using MiRs.Discord.Bot.Domain.Mappers;
using Microsoft.VisualBasic;

namespace MiRs.Discord.Bot.API.Controllers.Commands
{
    /// <summary>
    ///  Base module that gives access to MediatR object
    /// </summary>
    public abstract class BaseModule : ApplicationCommandModule<ApplicationCommandContext>
    {
        /// <summary>
        ///  gets the Mediator object
        /// </summary>
        protected ISender Mediator { get; }

        /// <summary>
        ///  gets the Appsetting object
        /// </summary>
        protected AppSettings Appsettings {  get; }

        /// <summary>
        /// Creates MediatR object
        /// </summary>
        protected BaseModule(ISender mediator, IOptions<AppSettings> appSettings)
        {
            Mediator = mediator;
            Appsettings = appSettings.Value;
        }

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
        [SlashCommand("ping", "Ping!")]
        public async Task Pong()
        {

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            await RespondAsync(InteractionCallback.DeferredMessage());

            StringBuilder sb = new StringBuilder();

            sb.Append("Doctor Mudkip....Online\n");

            try
            {
                IFlurlResponse response = await "https://localhost:7176/v1/"
                   .AppendPathSegment($"AdminRH/ping")
                   .WithTimeout(TimeSpan.FromSeconds(10))
                   .PostAsync();
                sb.Append("MiRs Api....Online\n");

            }
            catch (Exception ex) {

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
                Presence = new PresenceProperties(UserStatusType.Online)
            };

            await FollowupAsync(message);
        }

        /// <summary>
        /// Check if user is validated
        /// </summary>
        public async Task<bool> UserValidated()
        {
            if (Appsettings.DiscordSuperAdmins.Contains(Context.User.Id))
            {
                return true;
            }

           if((await Context.Client.Rest.GetGuildUserAsync(Context.Guild.Id, Context.User.Id)).GetPermissions(Context.Guild).HasFlag(Permissions.Administrator))
            {
                return true;
            }

            return await Task.FromResult(false);
        }
    }
}
