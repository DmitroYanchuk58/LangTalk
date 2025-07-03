using LangTalkMobileApp.DTOs;
using LangTalkMobileApp.Token;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LangTalkMobileApp.MVVM.ViewModel
{
    public class ChangeLanguagesViewModel : INotifyPropertyChanged
    {
        private TokenService tokenService;
        public ChangeLanguagesViewModel(TokenService tokenService)
        { 
            this.tokenService = tokenService;
            Task.Run(async () =>
            {
                var languages = await HttpCommands.Command.GetAllLanguages();
                foreach (var language in languages)
                {
                    Languages.Add(new Language { LanguageName = language });
                }
            });
        }

        public ObservableCollection<Language> Languages { get; set; } = new(); 

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public ICommand UpdateLanguagesCommand => new Microsoft.Maui.Controls.Command(async () =>
        {
            foreach (var language in Languages)
            {
                if (language.IsSelected)
                {
                    var idUser = tokenService.IdUser;
                    var idLanguageString = await HttpCommands.Command.GetLanguageId(language.LanguageName);
                    var idLanguage = Guid.Parse(idLanguageString.Trim('"'));
                    UserLanguage userLanguage = new UserLanguage()
                    {
                        IdLanguage = idLanguage,
                        IdUser = idUser,
                        IsSpoken = language.IsSpoken,
                        Level = language.Level
                    };
                    await HttpCommands.Command.CreateUserLanguage(userLanguage);

                    await ChangeViewCommand.ChangeViewCommand.ChangeOnProfileView(idUser);
                }
            }
        });

        public ICommand OnProfileCommand => new Command(async () =>
        {
            var idUser = tokenService.IdUser;
            await ChangeViewCommand.ChangeViewCommand.ChangeOnProfileView(idUser);
        });
    }
}
