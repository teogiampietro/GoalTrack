using GoalTrack.Publisher.Messages;

namespace GoalTrack.Consumer.Handler;

public class GoalHandler : IMessageHandler
{
    private readonly ILogger<GoalHandler> _logger;

    public GoalHandler(ILogger<GoalHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(IMessage message)
    {
        var goal = (Goal)message;
        _logger.LogInformation("Goal done! From {TeamName}", goal.TeamName);
        return Task.CompletedTask;
    }

    public static Type MessageType { get; } = typeof(GoalHandler);
}