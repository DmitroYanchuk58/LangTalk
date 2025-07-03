using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WpfAppDiplomaDemo.MVVM.Model
{
    public class ChatModel
    {
        [JsonPropertyName("id")]
        public Guid IdChat { get; set; }

        [JsonPropertyName("name")]
        public string ChatName { get; set; }

        [JsonPropertyName("messages")]
        public List<MessageModel> Messages { get; set; }

        [JsonPropertyName("userIds")]
        public List<Guid> UserIds { get; set; }
    }
}