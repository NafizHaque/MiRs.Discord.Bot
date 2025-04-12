using System.Reflection;
using Asp.Versioning;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MiRs.Discord.Bot.API.Controllers.Commands;
using MiRs.Discord.Bot.Interactors;
using NetCord;
using NetCord.Gateway;
using NetCord.Hosting.Gateway;
using NetCord.Hosting.Services;
using NetCord.Hosting.Services.ApplicationCommands;
using NetCord.Rest;

namespace MiRs.Discord.Bot.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "MiRS Discord Bot",
                    Description = "MiRs OSRS Discord bot companion as API.",
                    Version = "v1.0",
                });

                string xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

                options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                options.CustomSchemaIds(x => x.FullName);
            });

            builder.Services.AddApiVersioning(
                options =>
                {
                    options.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.ReportApiVersions = true;

                }).AddApiExplorer(options =>
                {
                    options.SubstituteApiVersionInUrl = true;
                });


            builder.Services
                .AddDiscordGateway(options =>
                {
                    options.Intents = GatewayIntents.GuildMessages
                                      | GatewayIntents.DirectMessages
                                      | GatewayIntents.MessageContent
                                      | GatewayIntents.DirectMessageReactions
                                      | GatewayIntents.GuildMessageReactions;
                })
                .AddGatewayEventHandlers(typeof(Program).Assembly)
                .AddApplicationCommands();

            builder.Services.AddMediatRContracts();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            // Add commands from modules
            app .AddModules(typeof(Program).Assembly);

            app.UseGatewayEventHandlers();

            await app.RunAsync();
        }
    }
}