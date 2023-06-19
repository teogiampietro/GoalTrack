using Amazon;
using Amazon.SQS;
using GoalTrack.Consumer;
using GoalTrack.Consumer.Handler;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<SqsConsumerService>();
builder.Services.AddSingleton<IAmazonSQS>(_ => new AmazonSQSClient(RegionEndpoint.USEast2));

builder.Services.AddSingleton<MessageDispatcher>();

builder.Services.AddScoped<GoalHandler>();
builder.Services.AddScoped<CardFaultHandler>();

var app = builder.Build();

app.Run();