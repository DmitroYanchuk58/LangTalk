using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangTalkMobileApp.MVVM.Model
{
    public class SelectPeopleModel
    {
        public Guid IdUser { get; set; }
        public string Nickname { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Country { get; set; }
        public string MbtiType { get; set; }
        public string MbtiDescription { get; set; }

        public List<string> Hobbies { get; set; }

        public List<string> SpokenLanguages { get; set; }

        public List<string> LearningLanguages { get; set; }

        public bool IsCompatibleType { get; set; }

        public bool DoesHaveSomeHobbies { get; set; }

        public int CountSameHobbies { get; set; }

        public ImageSource DisplayedImage { get; set; }

        public string SameHobbies => $"{CountSameHobbies} same hobbies";
    }
}
