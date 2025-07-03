using LangTalkMobileApp.MVVM.ViewModel;

namespace LangTalkMobileApp.MVVM.View;

public partial class SelectPeoplePage : ContentPage
{
	public SelectPeoplePage(SelectPeopleViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}