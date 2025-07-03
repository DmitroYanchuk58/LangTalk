using Microsoft.Maui.Controls;

namespace LangTalkMobileApp.DesignElements;

public partial class RoundedTextBox : ContentView
{
    public RoundedTextBox()
    {
        InitializeComponent();
        BindingContext = this;
    }

    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(RoundedTextBox), string.Empty);

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public static readonly BindableProperty PlaceholderProperty =
        BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(RoundedTextBox), string.Empty);

    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    public static readonly BindableProperty BoxHeightProperty =
        BindableProperty.Create(nameof(BoxHeight), typeof(double), typeof(RoundedTextBox), 50.0);

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
}
