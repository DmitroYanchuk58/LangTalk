using LangTalkMobileApp.DTOs;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LangTalkMobileApp.DTOs
{
    public class Chat
    {
        [JsonPropertyName("id")]
        public Guid IdChat { get; set; }

        [JsonPropertyName("name")]
        public string ChatName { get; set; }

        [JsonPropertyName("messages")]
        public List<Message> Messages { get; set; }

        [JsonPropertyName("userIds")]
        public List<Guid> UserIds { get; set; }
    }
}