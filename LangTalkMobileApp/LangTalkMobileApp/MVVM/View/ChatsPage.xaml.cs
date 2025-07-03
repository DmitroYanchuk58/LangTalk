using LangTalkMobileApp.MVVM.ViewModel;

namespace LangTalkMobileApp.MVVM.View;

public partial class ChatsPage : ContentPage
{
	public ChatsPage(ChatsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}