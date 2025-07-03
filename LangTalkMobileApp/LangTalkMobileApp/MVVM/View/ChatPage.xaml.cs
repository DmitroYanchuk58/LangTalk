using LangTalkMobileApp.MVVM.ViewModel;

namespace LangTalkMobileApp.MVVM.View;

public partial class ChatPage : ContentPage
{
	public ChatPage(ChatViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}