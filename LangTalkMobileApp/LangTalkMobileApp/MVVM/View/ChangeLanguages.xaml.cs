using LangTalkMobileApp.MVVM.ViewModel;

namespace LangTalkMobileApp.MVVM.View;

public partial class ChangeLanguagesPage : ContentPage
{
	public ChangeLanguagesPage(ChangeLanguagesViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}