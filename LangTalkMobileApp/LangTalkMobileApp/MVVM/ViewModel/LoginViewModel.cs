using CommunityToolkit.Mvvm.Input;
using LangTalkMobileApp.ChangeViewCommand;
using LangTalkMobileApp.HttpCommands;
using LangTalkMobileApp.MVVM.Model;
using LangTalkMobileApp.MVVM.View;
using LangTalkMobileApp.Token;
using System.ComponentModel;
using System.Windows.Input;

public partial class LoginViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private LoginModel model;

    private bool _isDataWrong = false;
    private TokenService _tokenService;

    public bool IsDataWrong
    {
        get => _isDataWrong;
        set
        {
            if (_isDataWrong != value)
            {
                _isDataWrong = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsDataWrong)));
            }
        }
    }

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

    [RelayCommand]
    public async Task ChangeOnRegistrationView()
    {
        await Shell.Current.GoToAsync(nameof(RegistrationPage));
    }

    public LoginViewModel(LoginModel model, TokenService tokenService)
    {
        this.model = model;
        _tokenService = tokenService;
    }


    [RelayCommand]
    public async Task Login()
    {
        if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password))
        {
            await Login(Email, Password);
        }
    }

    private async Task Login(string email, string password)
    {
        try
        {
            var token = await LangTalkMobileApp.HttpCommands.Command.LoginGetToken(Email, Password);
            _tokenService.Token = token;
            var idUser = _tokenService.IdUser;
            //Change view
            await ChangeViewCommand.ChangeOnProfileView(idUser);
        }
        catch (Exception ex)
        {
            IsDataWrong = true;
        }
    }
}
