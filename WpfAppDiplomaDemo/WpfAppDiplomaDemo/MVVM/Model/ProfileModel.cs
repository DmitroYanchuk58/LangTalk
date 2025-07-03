using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WpfAppDiplomaDemo.JsonModels;

namespace WpfAppDiplomaDemo.MVVM.Model
{
    public class ProfileModel
    {
        public Guid Id { get; set; }
        public string Nickname { get; set; }
        public string MbtiType { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Dictionary<string, string> LanguagesSpoken { get; set; }
        public Dictionary<string, string> LanguagesLearning { get; set; }

        public string Age { get; set; } 

        public string Gender { get; set; }

        public List<string> Hobbies { get; set; } = new List<string>();

        public string MbtiTypeDescription { get; set; }

        public ProfileModel(User? user, UserInfo? userInfo, MbtiType? mbtiType, List<Language> languages, List<string> hobbies)
        {
            Id = Guid.TryParse(user?.Id, out var parsedId) ? parsedId : Guid.Empty;
            Nickname = string.IsNullOrWhiteSpace(user?.Nickname) ? "Data hasn't been created yet" : user.Nickname;

            Country = string.IsNullOrWhiteSpace(userInfo?.Country) ? "Data hasn't been created yet" : userInfo.Country;
            Description = string.IsNullOrWhiteSpace(userInfo?.Country) ? "Default description" : userInfo.Description;

            FirstName = string.IsNullOrWhiteSpace(userInfo?.FirstName) ? "Data hasn't been created yet" : userInfo.FirstName;
            LastName = string.IsNullOrWhiteSpace(userInfo?.LastName) ? "Data hasn't been created yet" : userInfo.LastName;
            Gender = string.IsNullOrWhiteSpace(userInfo?.Gender) ? "Data hasn't been created yet" : userInfo.Gender;

            LanguagesSpoken = new Dictionary<string, string>();
            LanguagesLearning = new Dictionary<string, string>();

            Age = userInfo?.DateOfBirth != null
                ? (DateTime.Now.Year - userInfo.DateOfBirth.Year).ToString()
                : "1";

            MbtiType = string.IsNullOrWhiteSpace(mbtiType?.Type) ? "Data hasn't been created yet" : mbtiType.Type;
            MbtiTypeDescription = string.IsNullOrWhiteSpace(mbtiType?.Description) ? "Data hasn't been created yet" : mbtiType.Description;

            foreach(var language in languages)
            {
                if (language.IsSpoken)
                {
                    LanguagesSpoken[language.LanguageName] = language.Level;
                }
                else
                {
                    LanguagesLearning[language.LanguageName] = language.Level;
                }
            }

            Hobbies = hobbies;
        }

    }
}
