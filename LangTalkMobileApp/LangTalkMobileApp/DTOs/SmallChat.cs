using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LangTalkMobileApp.DTOs
{
    public class SmallChat
    {
        [JsonPropertyName("chatName")]
        public string Nickname { get; set; }

        [JsonPropertyName("lastMessage")]
        public string LastMessage { get; set; }

        [JsonPropertyName("idChat")]
        public Guid IdChat { get; set; }

        [JsonPropertyName("lastMessageDate")]
        public DateTime Time { get; set; }

        public string TimeLastMessage => Time.ToString("HH:mm");
    }
}
