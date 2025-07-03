using LangTalkMobileApp.ChangeViewCommand;
using LangTalkMobileApp.HttpCommands;
using LangTalkMobileApp.Token;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Input;
using Command = Microsoft.Maui.Controls.Command;

public class ChangeProfileViewModel : INotifyPropertyChanged
{
    private bool _isAddImageVisible = false;
    private TokenService _tokenService;

    public ChangeProfileViewModel(TokenService tokenService)
    {
        _tokenService = tokenService;
        DisplayedImage = ImageSource.FromFile("default_profile_image.jpg");
        Task.Run(async () =>
        {
            Countries = await LangTalkMobileApp.HttpCommands.Command.GetAllCountries();
            OnPropertyChanged(nameof(Countries));
        });
    }


    public bool IsAddImageVisible
    {
        get => _isAddImageVisible;
        set
        {
            if (_isAddImageVisible != value)
            {
                _isAddImageVisible = value;
                OnPropertyChanged(nameof(IsAddImageVisible));
            }
        }
    }

    public ICommand ChangeAddImageVisibilityCommand => new Command(() =>
    {
        IsAddImageVisible = !IsAddImageVisible;
    });

    #region Properties

    private ImageSource _displayedImage;
    public ImageSource DisplayedImage
    {
        get => _displayedImage;
        set
        {
            if (_displayedImage != value)
            {
                _displayedImage = value;
                OnPropertyChanged(nameof(DisplayedImage));
            }
        }
    }

    private byte[] _imageBytes;
    public byte[] ImageBytes
    {
        get => _imageBytes;
        set
        {
            if (_imageBytes != value)
            {
                _imageBytes = value;
                OnPropertyChanged(nameof(ImageBytes));
            }
        }
    }

    private string _firstName;
    public string FirstName
    {
        get => _firstName;
        set
        {
            if (_firstName != value)
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }
    }

    private string _lastName;
    public string LastName
    {
        get => _lastName;
        set
        {
            if (_lastName != value)
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }
    }

    private int _age;
    public int Age
    {
        get => _age;
        set
        {
            if (_age != value)
            {
                _age = value;
                OnPropertyChanged(nameof(Age));
            }
        }
    }

    private List<string> _countries;
    public List<string> Countries
    {
        get => _countries;
        set
        {
            _countries = value;
            OnPropertyChanged(nameof(Countries));
        }
    }

    private string _selectedCountry;
    public string Country
    {
        get => _selectedCountry;
        set
        {
            if (_selectedCountry != value)
            {
                _selectedCountry = value;
                OnPropertyChanged(nameof(Country));
            }
        }
    }

    private string _description;
    public string Description
    {
        get => _description;
        set
        {
            if (_description != value)
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
    }


    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    #endregion

    public async Task PickPhotoAsync()
    {
        try
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult photo = await MediaPicker.Default.PickPhotoAsync();

                if (photo != null)
                {
                    using Stream stream = await photo.OpenReadAsync();

                    using MemoryStream ms = new();
                    await stream.CopyToAsync(ms);
                    ImageBytes = ms.ToArray();

                    DisplayedImage = ImageSource.FromStream(() => new MemoryStream(ImageBytes));
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"PickPhotoAsync ERROR: {ex.Message}");
        }
    }


    public ICommand PickPhotoCommand => new Command(async () => await PickPhotoAsync());

    public ICommand OnProfileCommand => new Command(async () =>
    {
        var idUser = _tokenService.IdUser;
        await ChangeViewCommand.ChangeOnProfileView(idUser);
    });

    public ICommand UpdateProfileCommand => new Command(async () =>
    {
        var idUser = _tokenService.IdUser;
        await LangTalkMobileApp.HttpCommands.Command.CreateUserImage(idUser, ImageBytes);
        await LangTalkMobileApp.HttpCommands.Command.ChangeUserInfo(idUser,"Male",Description,FirstName,LastName,Country,DateTime.Today.AddYears(-Age));
        await ChangeViewCommand.ChangeOnProfileView(idUser);
    });
}
