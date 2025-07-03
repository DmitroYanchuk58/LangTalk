using CommunityToolkit.Mvvm.ComponentModel;
using LangTalkMobileApp.MVVM.ViewModel;
using System.ComponentModel;

namespace LangTalkMobileApp.MVVM.View;

public partial class ProfilePage : ContentPage
{
    public ProfilePage(ProfileViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
