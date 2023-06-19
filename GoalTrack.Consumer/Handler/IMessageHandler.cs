using GoalTrack.Publisher.Messages;

namespace GoalTrack.Consumer.Handler;

public interface IMessageHandler
{
    public Task HandleAsync(IMessage message);
    
    public static Type MessageType { get; } = default!;
}