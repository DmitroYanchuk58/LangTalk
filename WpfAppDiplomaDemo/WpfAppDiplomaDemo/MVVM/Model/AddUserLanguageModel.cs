using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppDiplomaDemo.MVVM.Model
{
    public class AddUserLanguageModel
    {
        public List<string> Languages { get; set; }

        public List<string> Level { get; set; }

        public AddUserLanguageModel(List<string> languages)
        {
            Languages = languages;
            Level = new List<string>
            {
                "A1",
                "A2",
                "B1",
                "B2",
                "C1",
                "C2",
                "N"
            };
        }
    }
}
