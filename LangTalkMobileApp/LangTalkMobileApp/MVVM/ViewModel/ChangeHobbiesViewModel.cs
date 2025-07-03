using LangTalkMobileApp.DTOs;
using LangTalkMobileApp.MVVM.View;
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
    public class ChangeHobbiesViewModel : INotifyPropertyChanged
    {
        private TokenService _tokenService;
        public ChangeHobbiesViewModel(TokenService tokenService)
        {
            _tokenService = tokenService;
            LoadHobbies(); 
        }

        private async void LoadHobbies()
        {
            try
            {
                var hobbies = await LangTalkMobileApp.HttpCommands.Command.GetAllHobbies();
                foreach (var hobby in hobbies)
                {
                    Hobbies.Add(new Hobby
                    {
                        Name = hobby,
                        IsUser = false
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading hobbies: " + ex.Message);
            }
        }


        public ICommand OnProfileCommand => new Command(async () =>
        {
            var idUser = _tokenService.IdUser;
            await ChangeViewCommand.ChangeViewCommand.ChangeOnProfileView(idUser);
        });

        public ObservableCollection<Hobby> Hobbies { get; set; } = new();

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        public ICommand ToggleHobbyCommand => new Command<Hobby>((hobby) =>
        {
            if (hobby != null)
                hobby.IsUser = !hobby.IsUser;
        });

        public ICommand UpdateHobbiesCommand => new Microsoft.Maui.Controls.Command(async () =>
        {
            var idUser = _tokenService.IdUser;
            foreach (var hobby in Hobbies.Where(h => h.IsUser))
            {
                var idHobbyString = await LangTalkMobileApp.HttpCommands.Command.GetHobbyIdByName(hobby.Name);
                var idHobby = Guid.Parse(idHobbyString);
                await LangTalkMobileApp.HttpCommands.Command.CreateUserHobby(idUser, idHobby);
            }
            await ChangeViewCommand.ChangeViewCommand.ChangeOnProfileView(idUser);
        });


    }
}
