namespace LangTalkMobileApp.DesignElements;

public partial class BottomMenu : ContentView
{
	public BottomMenu()
	{
		InitializeComponent();
	}

    public static readonly BindableProperty SearchCommandProperty =
        BindableProperty.Create(nameof(SearchCommand), typeof(Command), typeof(BottomMenu));

    public static readonly BindableProperty ProfileCommandProperty =
        BindableProperty.Create(nameof(ProfileCommand), typeof(Command), typeof(BottomMenu));

    public static readonly BindableProperty ChatsCommandProperty =
        BindableProperty.Create(nameof(ChatsCommand), typeof(Command), typeof(BottomMenu));

    public Command SearchCommand
    {
        get => (Command)GetValue(SearchCommandProperty);
        set => SetValue(SearchCommandProperty, value);
    }

    public Command ProfileCommand
    {
        get => (Command)GetValue(ProfileCommandProperty);
        set => SetValue(ProfileCommandProperty, value);
    }

    public Command ChatsCommand
    {
        get => (Command)GetValue(ChatsCommandProperty);
        set => SetValue(ChatsCommandProperty, value);
    }

}