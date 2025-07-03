using System;
using System.Text.Json.Serialization;

namespace LangTalkMobileApp.DTOs
{
    public class User
    {
        [JsonPropertyName("nickname")]
        public string Nickname { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("idInfo")]
        public string? IdInfo { get; set; }
    }
}
