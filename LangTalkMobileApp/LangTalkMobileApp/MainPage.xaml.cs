using CommunityToolkit.Mvvm.Input;
using LangTalkMobileApp.MVVM.View;

namespace LangTalkMobileApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        [RelayCommand]
        public async Task ChangeOnLoginView()
        {
            await Shell.Current.GoToAsync(nameof(LoginPage));
        }

        [RelayCommand]
        public async Task ChangeOnRegistrationView()
        {
            await Shell.Current.GoToAsync(nameof(RegistrationPage));
        }

        [RelayCommand]
        public async Task ChangeOnProfileView()
        {
            await Shell.Current.GoToAsync(nameof(ProfilePage));
        }

        [RelayCommand]
        public async Task ChangeOnChangeProfileView()
        {
            await Shell.Current.GoToAsync(nameof(ChangeProfilePage));
        }

        [RelayCommand]
        public async Task ChangeOnChangeMbtiTypeView()
        {
            await Shell.Current.GoToAsync(nameof(ChangeMbtiTypePage));
        }

        [RelayCommand]
        public async Task ChangeOnChangeHobbiesView()
        {
            await Shell.Current.GoToAsync(nameof(ChangeHobbiesPage));
        }

        [RelayCommand]
        public async Task ChangeOnChatsView()
        {
            await Shell.Current.GoToAsync(nameof(ChatsPage));
        }

        [RelayCommand]
        public async Task ChangeOnChatView()
        {
            await Shell.Current.GoToAsync(nameof(ChatPage));
        }

        [RelayCommand]
        public async Task ChangeOnSelectPeopleView()
        {
            await Shell.Current.GoToAsync(nameof(SelectPeoplePage));
        }
    }
}
