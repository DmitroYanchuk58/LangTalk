using System.Windows.Input;

namespace LangTalkMobileApp.DesignElements;

public partial class RoundedBrownButton : ContentView
{
	public RoundedBrownButton()
	{
		InitializeComponent();
        BindingContext = this;
    }

    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(RoundedBrownButton), string.Empty);

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public static readonly BindableProperty BoxHeightProperty =
    BindableProperty.Create(nameof(BoxHeight), typeof(double), typeof(RoundedTextBox), 300.0);

    public double BoxHeight
    {
        get => (double)GetValue(BoxHeightProperty);
        set => SetValue(BoxHeightProperty, value);
    }

    public static readonly BindableProperty BoxWidthProperty =
        BindableProperty.Create(nameof(BoxWidth), typeof(double), typeof(RoundedTextBox), 300.0);

    public double BoxWidth
    {
        get => (double)GetValue(BoxWidthProperty);
        set => SetValue(BoxWidthProperty, value);
    }

    public static readonly BindableProperty FontSizeProperty =
        BindableProperty.Create(nameof(FontSize), typeof(double), typeof(RoundedTextBox), 300.0);

    public double FontSize
    {
        get => (double)GetValue(FontSizeProperty);
        set => SetValue(FontSizeProperty, value);
    }

    public static readonly BindableProperty CommandProperty =
    BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(RoundedTextBox), null);

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

}