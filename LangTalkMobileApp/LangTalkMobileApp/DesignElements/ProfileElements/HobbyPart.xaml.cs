namespace LangTalkMobileApp.DesignElements.ProfileElements;

public partial class HobbyPart : ContentView
{
    public static readonly BindableProperty HobbiesProperty =
    BindableProperty.Create(nameof(Hobbies), typeof(string), typeof(HobbyPart), string.Empty);

    public List<string> Hobbies
    {
        get => (List<string>)GetValue(HobbiesProperty);
        set => SetValue(HobbiesProperty, value);
    }

    public static readonly BindableProperty HobbyScrollMinHeightProperty =
    BindableProperty.Create(nameof(HobbyScrollMinHeight), typeof(double), typeof(HobbyPart), 50.0);

    public static readonly BindableProperty HobbyScrollMaxHeightProperty =
        BindableProperty.Create(nameof(HobbyScrollMaxHeight), typeof(double), typeof(HobbyPart), 150.0);

    public double HobbyScrollMinHeight
    {
        get => (double)GetValue(HobbyScrollMinHeightProperty);
        set => SetValue(HobbyScrollMinHeightProperty, value);
    }

    public double HobbyScrollMaxHeight
    {
        get => (double)GetValue(HobbyScrollMaxHeightProperty);
        set => SetValue(HobbyScrollMaxHeightProperty, value);
    }

    public HobbyPart()
    {
        InitializeComponent();
    }
}