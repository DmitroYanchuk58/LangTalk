using LangTalkMobileApp.MVVM.ViewModel;

namespace LangTalkMobileApp.MVVM.View;

public partial class ChangeMbtiTypePage : ContentPage
{
	public ChangeMbtiTypePage(ChangeMbtiTypeViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}