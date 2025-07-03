using System;
using System.Text.Json.Serialization;

namespace WpfAppDiplomaDemo.MVVM.Model
{
    public class MessageModel
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
    }
}