using System.Net;
using System.Text.Json;
using Amazon.SQS;
using Amazon.SQS.Model;
using GoalTrack.Publisher.Messages;

namespace GoalTrack.Consumer;

public class SqsConsumerService : BackgroundService
{
    private readonly IAmazonSQS _sqs;
    private readonly MessageDispatcher _dispatcher;
    private const string QueueName = "GoalTrackerSQS";
    private readonly List<string> _messageAttributeNames = new() { "All" };

    public SqsConsumerService(IAmazonSQS sqs, MessageDispatcher dispatcher)
    {
        _sqs = sqs;
        _dispatcher = dispatcher;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var queueUrl = await _sqs.GetQueueUrlAsync(QueueName, cancellationToken);
        var receiveRequest = new ReceiveMessageRequest()
        {
            QueueUrl = queueUrl.QueueUrl,
            MessageAttributeNames = _messageAttributeNames,
            AttributeNames = _messageAttributeNames
        };

        while (!cancellationToken.IsCancellationRequested)
        {
            var messageResponse = await _sqs.ReceiveMessageAsync(receiveRequest, cancellationToken);
            if (messageResponse.HttpStatusCode != HttpStatusCode.OK)
            {
                //Handle error
                continue;
            }

            foreach (var message in messageResponse.Messages)
            {
                var messageTypeName = message.MessageAttributes
                    .GetValueOrDefault(nameof(IMessage.MessageTypeName))?.StringValue;

                if (messageTypeName is null)
                {
                    //Log why its null
                    continue;
                }

                if (!_dispatcher.CanHandleMessageType(messageTypeName))
                {
                    //Log message cant be handled
                    continue;
                }

                var messageType = _dispatcher.GetMessageTypeByName(messageTypeName)!;

                var messageAsType = (IMessage)JsonSerializer.Deserialize(message.Body, messageType)!;
                
                await _dispatcher.DispatchAsync(messageAsType);
                //await _sqs.DeleteMessageAsync(queueUrl.QueueUrl, message.ReceiptHandle, cancellationToken);
            }
        }
    }
}