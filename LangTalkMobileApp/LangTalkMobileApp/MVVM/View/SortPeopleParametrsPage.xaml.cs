using LangTalkMobileApp.MVVM.ViewModel;

namespace LangTalkMobileApp.MVVM.View;

public partial class SortPeopleParametrsPage : ContentPage
{
	public SortPeopleParametrsPage(SortPeopleParametrsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}