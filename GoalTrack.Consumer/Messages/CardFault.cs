using System.Text.Json.Serialization;

namespace GoalTrack.Publisher.Messages;

public class CardFault : IMessage
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("teamName")]
    public string TeamName { get; init; } = default!;
    [JsonPropertyName("playerName")]
    public string PlayerName { get; init; } = default!;
    [JsonPropertyName("redCard")]
    public bool RedCard { get; init; } = default!;
    [JsonIgnore] public string MessageTypeName => nameof(CardFault);
}