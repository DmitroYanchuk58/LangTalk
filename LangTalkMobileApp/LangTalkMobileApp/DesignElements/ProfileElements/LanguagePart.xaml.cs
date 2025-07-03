namespace LangTalkMobileApp.DesignElements.ProfileElements;

public partial class LanguagePart : ContentView
{
    public static readonly BindableProperty SpokenLanguagesProperty =
        BindableProperty.Create(nameof(SpokenLanguages), typeof(string), typeof(LanguagePart), string.Empty);

    public List<string> SpokenLanguages
    {
        get => (List<string>)GetValue(SpokenLanguagesProperty);
        set => SetValue(SpokenLanguagesProperty, value);
    }

    public static readonly BindableProperty LearningLanguagesProperty =
        BindableProperty.Create(nameof(LearningLanguages), typeof(string), typeof(LanguagePart), string.Empty);

    public List<string> LearningLanguages
    {
        get => (List<string>)GetValue(LearningLanguagesProperty);
        set => SetValue(LearningLanguagesProperty, value);
    }
    public LanguagePart()
	{
		InitializeComponent();
	}
}