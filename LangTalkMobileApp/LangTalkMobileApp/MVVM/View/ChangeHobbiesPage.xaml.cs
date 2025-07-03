using LangTalkMobileApp.MVVM.ViewModel;

namespace LangTalkMobileApp.MVVM.View;

public partial class ChangeHobbiesPage : ContentPage
{
	public ChangeHobbiesPage(ChangeHobbiesViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}