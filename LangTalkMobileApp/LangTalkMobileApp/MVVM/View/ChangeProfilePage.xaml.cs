using LangTalkMobileApp.MVVM.ViewModel;

namespace LangTalkMobileApp.MVVM.View;

public partial class ChangeProfilePage : ContentPage
{
	public ChangeProfilePage(ChangeProfileViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }

}