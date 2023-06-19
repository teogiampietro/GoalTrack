using System.Text.Json.Serialization;

namespace GoalTrack.Publisher.Messages;

public class Goal : IMessage
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("teamName")]
    public string TeamName { get; init; } = default!;

    [JsonIgnore] public string MessageTypeName => nameof(Goal);
}