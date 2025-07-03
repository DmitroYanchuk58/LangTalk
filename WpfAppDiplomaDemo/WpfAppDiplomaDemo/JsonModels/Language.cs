using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WpfAppDiplomaDemo.JsonModels
{
    public class Language
    {
        [JsonPropertyName("languageName")]
        public string LanguageName { get; set; } = null!;

        [JsonPropertyName("isSpoken")]
        public bool IsSpoken { get; set; }

        [JsonPropertyName("level")]
        public string Level { get; set; } = null!;
    }
}
