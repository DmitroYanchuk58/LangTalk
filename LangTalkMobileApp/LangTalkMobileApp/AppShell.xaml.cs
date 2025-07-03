using LangTalkMobileApp.MVVM.View;

namespace LangTalkMobileApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));

            Routing.RegisterRoute(nameof(RegistrationPage), typeof(RegistrationPage));

            Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));

            Routing.RegisterRoute(nameof(ChangeProfilePage), typeof(ChangeProfilePage));

            Routing.RegisterRoute(nameof(ChangeMbtiTypePage), typeof(ChangeMbtiTypePage));

            Routing.RegisterRoute(nameof(ChangeHobbiesPage), typeof(ChangeHobbiesPage));

            Routing.RegisterRoute(nameof(ChatsPage), typeof(ChatsPage));

            Routing.RegisterRoute(nameof(ChatPage), typeof(ChatPage));

            Routing.RegisterRoute(nameof(SelectPeoplePage), typeof(SelectPeoplePage));

            Routing.RegisterRoute(nameof(ChangeLanguagesPage), typeof(ChangeLanguagesPage));

            Routing.RegisterRoute(nameof(SortPeopleParametrsPage), typeof(SortPeopleParametrsPage));
        }
    }
}
