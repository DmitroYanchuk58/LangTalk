using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LangTalkMobileApp.MVVM.Model;
using LangTalkMobileApp.MVVM.View;
using LangTalkMobileApp.Token;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangTalkMobileApp.MVVM.ViewModel
{
    public partial class RegistrationViewModel : INotifyPropertyChanged
    {
        private RegistrationModel model;

        public event PropertyChangedEventHandler PropertyChanged;

        private TokenService _tokenService;

        public string Email
        {
            get => model.Email;
            set
            {
                if (model.Email != value)
                {
                    model.Email = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Email)));
                }
            }
        }

        public string Nickname
        {
            get => model.Nickname;
            set
            {
                if (model.Nickname != value)
                {
                    model.Nickname = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Nickname)));
                }
            }
        }

        public string Password
        {
            get => model.Password;
            set
            {
                if (model.Password != value)
                {
                    model.Password = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Password)));
                }
            }
        }

        public string ConfirmPassword
        {
            get => model.ConfirmPassword;
            set
            {
                if (model.ConfirmPassword != value)
                {
                    model.ConfirmPassword = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ConfirmPassword)));
                }
            }
        }

        [RelayCommand]
        public async Task ChangeOnRegistrationView()
        {
            await Shell.Current.GoToAsync(nameof(RegistrationPage));
        }

        public RegistrationViewModel(RegistrationModel model, TokenService tokenService)
        {
            this.model = model;
            _tokenService = tokenService;
        }


        [RelayCommand]
        public async Task Register()
        {
            if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password) && !string.IsNullOrWhiteSpace(Nickname) && !string.IsNullOrWhiteSpace(ConfirmPassword)
                && Password == ConfirmPassword)
            {
                await Register(Email, Password);
            }
        }

        private async Task Register(string email, string password)
        {
            try
            {
                var token = await LangTalkMobileApp.HttpCommands.Command.RegistrationAndGetToken(Email, Password,Nickname);
                _tokenService.Token = token;
                var idUser = _tokenService.IdUser;
                //Change view
                await ChangeViewCommand.ChangeViewCommand.ChangeOnProfileView(idUser);
            }
            catch (Exception ex)
            {
            }
        }

        [RelayCommand]
        public async Task ChangeOnLoginView()
        {
            await Shell.Current.GoToAsync(nameof(LoginPage));
        }
    }
}
