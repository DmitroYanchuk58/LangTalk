using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangTalkMobileApp.DTOs
{
    public class SortPeopleParameters
    {
        public List<string> SelectedCountries { get; set; }
        public List<string> SelectedMbtiTypes { get; set; }
        public List<string> SelectedHobbies { get; set; }
        public List<string> SelectedLanguages { get; set; }
        public List<string> SelectedGenders { get; set; }
    }
}
