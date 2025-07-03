using System;
using System.Text.Json.Serialization;

namespace LangTalkMobileApp.DTOs
{
    public class Message
    {
        [JsonPropertyName("idMessage")]
        public Guid IdMessage { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("timeSend")]
        public DateTime TimeSend { get; set; }

        [JsonPropertyName("idUser")]
        public Guid IdUser { get; set; }

        [JsonPropertyName("userName")]
        public string UserName { get; set; }

        public string Time => TimeSend.ToString("HH:MM");
    }
}