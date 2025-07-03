namespace LangTalkMobileApp.DesignElements.ProfileElements;

public partial class MbtiPart : ContentView
{
    public static readonly BindableProperty MbtiTypeProperty =
    BindableProperty.Create(nameof(MbtiType), typeof(string), typeof(DescriptionPart), string.Empty);

    public string MbtiType
    {
        get => (string)GetValue(MbtiTypeProperty);
        set => SetValue(MbtiTypeProperty, value);
    }

    public static readonly BindableProperty MbtiDescriptionProperty =
    BindableProperty.Create(nameof(MbtiDescription), typeof(string), typeof(DescriptionPart), string.Empty);

    public string MbtiDescription
    {
        get => (string)GetValue(MbtiTypeProperty);
        set => SetValue(MbtiTypeProperty, value);
    }

    public MbtiPart()
    {
        InitializeComponent();
    }
}