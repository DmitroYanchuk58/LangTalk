namespace LangTalkMobileApp.DesignElements.ProfileElements;

public partial class DescriptionPart : ContentView
{
    public static readonly BindableProperty DescriptionProperty =
        BindableProperty.Create(nameof(Description), typeof(string), typeof(DescriptionPart), string.Empty);

    public string Description
    {
        get => (string)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    public DescriptionPart()
    {
        InitializeComponent();
    }
}
