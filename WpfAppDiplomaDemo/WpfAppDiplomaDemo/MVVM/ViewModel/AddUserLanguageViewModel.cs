using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAppDiplomaDemo.Command;
using WpfAppDiplomaDemo.HttpCommands;
using WpfAppDiplomaDemo.JsonModels;
using WpfAppDiplomaDemo.MVVM.Model;

namespace WpfAppDiplomaDemo.MVVM.ViewModel
{
    public class AddUserLanguageViewModel
    {
        private MainViewModel MainVm;
        private AddUserLanguageModel model;
        public AddUserLanguageViewModel(MainViewModel mainVm, AddUserLanguageModel model)
        {
            MainVm = mainVm;
            Languages = model.Languages;
            Levels = model.Level;
        }

        public List<string> Languages { get; set; } = new List<string>();

        public List<string> Levels { get; set; } = new List<string>();

        public async void AddLanguage(string language,string level, bool isSpoken)
        {
            var idUser = MainVm.GetUserIdFromToken(MainVm.Token);
            var idLanguage = Commands.GetLanguageId(language).Result;
            UserLanguage userLanguage = new UserLanguage()
            {
                IdUser = idUser.Value,
                IdLanguage = Guid.Parse(idLanguage.Trim('"')),
                IsSpoken = isSpoken,
                Level = level
            };
            await Commands.CreateUserLanguage(userLanguage);
        }

        public async void ChangeViewToProfile()
        {
            await ChangeViewCommand.ChangeViewToProfile(MainVm);
        }
    }
}
