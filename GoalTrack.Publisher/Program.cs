using Amazon;
using Amazon.SQS;
using GoalTrack.Publisher;
using GoalTrack.Publisher.Messages;

var sqsClient = new AmazonSQSClient(RegionEndpoint.USEast2);

var publisher = new SqsPublisher(sqsClient);

await publisher.PublishAsync("GoalTrackerSQS", new Goal
{
    Id = 1,
    TeamName = "ARG"
});

await Task.Delay(5000);

await publisher.PublishAsync("GoalTrackerSQS", new CardFault()
{
    Id = 1,
    PlayerName = "Messi",
    TeamName = "ARG"
});