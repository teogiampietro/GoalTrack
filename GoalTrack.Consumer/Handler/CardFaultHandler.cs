using GoalTrack.Publisher.Messages;

namespace GoalTrack.Consumer.Handler;

public class CardFaultHandler : IMessageHandler
{
    private readonly ILogger<CardFaultHandler> _logger;

    public CardFaultHandler(ILogger<CardFaultHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(IMessage message)
    {
        var cardFault = (CardFault)message;
        _logger.LogInformation("Fault! Card to {TeamName}, player: {PlayerName}", new string[] {cardFault.TeamName, cardFault.PlayerName});
        return Task.CompletedTask;
    }

    public static Type MessageType { get; } = typeof(CardFaultHandler);
} 