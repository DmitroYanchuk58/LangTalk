using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WpfAppDiplomaDemo.JsonModels
{
    public class UserInfo
    {
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("gender")]
        public string Gender { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("dateOfBirth")]
        public DateTime DateOfBirth { get; set; }

        [JsonPropertyName("timeLastActivity")]
        public DateTime? TimeLastActivity { get; set; }

        [JsonPropertyName("idMbtiType")]
        public Guid? IdMbtiType { get; set; }

        [JsonPropertyName("idUser")]
        public Guid IdUser { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}

