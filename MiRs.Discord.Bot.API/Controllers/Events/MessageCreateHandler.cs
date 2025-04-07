using Microsoft.Extensions.Logging;

using NetCord.Gateway;
using NetCord.Hosting.Gateway;

namespace MyBot;

[GatewayEvent(nameof(GatewayClient.MessageCreate))]
public class MessageCreateHandler(ILogger<MessageCreateHandler> logger) : IGatewayEventHandler<Message>
{
    public ValueTask HandleAsync(Message message)
    {
        logger.LogInformation("'{message}' -  By {user}:{userid}  - In {channel}", 
            message.Content, message.Author.Username, message.Author.Id, message.ChannelId);
        return default;
    }
}
