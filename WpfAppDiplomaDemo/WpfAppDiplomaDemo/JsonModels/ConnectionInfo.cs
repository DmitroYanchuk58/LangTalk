using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WpfAppDiplomaDemo.JsonModels
{
    public class ConnectionInfo
    {
        [JsonPropertyName("idSecondUser")]
        public Guid IdSecondUser { get; set; }

        [JsonPropertyName("countSameHobbies")]
        public int CountSameHobbies { get; set; }

        [JsonPropertyName("score")]
        public double Score { get; set; }

        [JsonPropertyName("isAppropriateType")]
        public bool IsAppropriateType { get; set; }
    }
}
