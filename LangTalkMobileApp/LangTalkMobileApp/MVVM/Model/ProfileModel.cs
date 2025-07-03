using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangTalkMobileApp.MVVM.Model
{
    public class ProfileModel
    {
        public string FullName { get; set; }
        public int Age {  get; set; }
        public string Country {  get; set; }
        public string Gender {  get; set; }
        public string Description {  get; set; }
        public string MbtiType {  get; set; }
        public string MbtiDescription {  get; set; }

        public List<string> Hobbies { get; set; }

        public List<string> SpokenLanguages { get; set; }

        public List<string> LearningLanguages { get; set; }
    }
}
