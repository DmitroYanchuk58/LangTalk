using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAppDiplomaDemo.MVVM.Model;

namespace WpfAppDiplomaDemo.MVVM.ViewModel
{
    public class ProfileViewModel
    {
        private ProfileModel _model;
        public MainViewModel MainVM { get; set; }

        public BottomMenuViewModel BottomMenuVM => new BottomMenuViewModel(MainVM);

        public string Nickname => _model.Nickname;
        public string MbtiType => _model.MbtiType;
        public string Country => _model.Country;
        public string Description => _model.Description;

        public string FirstName => _model.FirstName;

        public string LastName => _model.LastName;

        public Dictionary<string, string> LanguagesSpoken => _model.LanguagesSpoken;

        public Dictionary<string, string> LanguagesLearning => _model.LanguagesLearning;

        public string Age => _model.Age;

        public string Gender => _model.Gender;

        public List<string> Hobbies => _model.Hobbies;

        public string MbtiTypeDescription => _model.MbtiTypeDescription;

        public ProfileViewModel(ProfileModel model, MainViewModel mainViewModel) 
        {
            _model = model;
            MainVM = mainViewModel;
        }
    }
}
