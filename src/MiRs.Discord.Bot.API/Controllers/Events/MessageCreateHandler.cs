using NetCord.Gateway;
using NetCord.Hosting.Gateway;

namespace MiRs.Discord.Bot.API.Controllers.Events;

/// <summary>
/// Logs Discord user message.
/// </summary>
public class MessageCreateHandler(ILogger<MessageCreateHandler> logger) : IMessageCreateGatewayHandler
{
    public ValueTask HandleAsync(Message message)
    {

        logger.LogInformation("'{message}' -  By {user}:{userid}  - In {channel}",
            message.Content, message.Author.Username, message.Author.Id, message.ChannelId);

        return default;
    }
}
