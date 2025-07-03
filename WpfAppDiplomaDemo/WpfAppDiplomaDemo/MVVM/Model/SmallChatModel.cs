using System.Text.Json;
using System.Text.Json.Serialization;

public class SmallChatModel
{
    [JsonPropertyName("chatName")]
    public string Nickname { get; set; }

    [JsonPropertyName("lastMessage")]
    public string LastMessage { get; set; }

    [JsonPropertyName("idChat")]
    public Guid IdChat { get; set; }

    [JsonPropertyName("lastMessageDate")]
    public DateTime Time { get; set; }
}