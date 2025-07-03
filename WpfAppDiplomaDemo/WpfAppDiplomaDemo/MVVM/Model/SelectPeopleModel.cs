using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppDiplomaDemo.MVVM.Model
{
    public class SelectPeopleModel
    {
        public string Name {  get; set; }

        public string MbtiType { get; set; }

        public string Country { get; set; }

        public string Description { get; set; }

        public List<string> Hobbies { get; set; }

        public string SameHobbiesCount { get; set; }

        public bool IsMbtiTypeCompatible { get; set; }

        public SelectPeopleModel(string name, string mbtiType, string country, string description, List<string> hobbies, int sameHobbiesCount, bool isMbtiTypeCompatible)
        {
            Name = name;
            MbtiType = mbtiType;
            Country = country;
            Description = description;
            Hobbies = hobbies;
            SameHobbiesCount = $"{sameHobbiesCount} Shared Hobbies";
            IsMbtiTypeCompatible = isMbtiTypeCompatible;
        }
    }
}
