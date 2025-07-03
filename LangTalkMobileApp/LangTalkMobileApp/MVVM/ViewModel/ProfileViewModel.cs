using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LangTalkMobileApp.DTOs;
using LangTalkMobileApp.MVVM.Model;
using LangTalkMobileApp.MVVM.View;
using LangTalkMobileApp.Token;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace LangTalkMobileApp.MVVM.ViewModel
{
    public partial class ProfileViewModel : INotifyPropertyChanged, IQueryAttributable
    {
        private Guid IdUser { get; set; }
        private ProfileModel _model;
        private TokenService _tokenService;

        public event PropertyChangedEventHandler PropertyChanged;

        public ProfileViewModel(TokenService tokenService)
        {
            _model = new ProfileModel();
            ActiveColor = Color.FromArgb("#FFB3BA");
            InactiveColor = Color.FromArgb("#D4A5A5");
            _hobbyColor = InactiveColor;
            _infoColor = ActiveColor;
            _tokenService = tokenService;
            DisplayedImage = ImageSource.FromFile("default_profile_image.jpg");
        }

        #region Get information abour user

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query != null)
            {
                IdUser = Guid.Parse(query["IdUser"].ToString());
                OnPropertyChanged(nameof(IdUser));

                try
                {
                    await LoadUser(IdUser);
                }
                catch { }
            }
        }

        private async Task LoadUser(Guid idUser)
        {
            var user = await HttpCommands.Command.GetUser(idUser);

            if(user.IdInfo != null)
            {
                Guid idUserInfo = Guid.Parse(user.IdInfo);
                var userInfo = await HttpCommands.Command.GetUserInfo(idUserInfo);
                await SetUserInfo(userInfo);
            }
            else
            {
                SetDefaultUserInfo();
                SetDefaultMbtiType();
            }
            var hobbies = await HttpCommands.Command.GetUserHobbies(idUser);
            if (hobbies.Count > 0)
            {
                SetHobbies(hobbies);
            }
            else
            {
                SetDefaultHobbies();
            }
            var languages = await HttpCommands.Command.GetUserLanguages(idUser);
            if (languages.Count > 0)
            {
                SetLanguages(languages);
            }
            else
            {
                SetDefaultLanguages();
            }
            var image = await HttpCommands.Command.GetUserImage(idUser);
            LoadImageFromBytes(image);
        }

        public void LoadImageFromBytes(byte[] bytes)
        {
            if (bytes != null && bytes.Length > 0)
            {
                DisplayedImage = ImageSource.FromStream(() => new MemoryStream(bytes));
            }
        }

        private void SetHobbies(List<string> hobbies)
        {
            Hobbies = hobbies;
        }

        private async Task SetUserInfo(UserInfo userInfo)
        {
            if (userInfo.DateOfBirth != null)
            {
                var age = DateTime.Today.Year - userInfo.DateOfBirth.Year;
                Age = age;
            }
            else
            {
                Age = 20;
            }

            if(!string.IsNullOrWhiteSpace(userInfo.FirstName) && !string.IsNullOrWhiteSpace(userInfo.LastName))
            {
                FullName = userInfo.FirstName + " " + userInfo.LastName;
            }
            else
            {
                FullName = "John Doe";
            }

            if (!string.IsNullOrWhiteSpace(userInfo.Gender))
            {
                Gender = userInfo.Gender;
            }
            else
            {
                Gender = "Male";
            }

            if (!string.IsNullOrWhiteSpace(userInfo.Country))
            {
                Country = userInfo.Country;
            }
            else 
            {
                Country = "Ukraine";
            }

            if (!string.IsNullOrWhiteSpace(userInfo.Description))
            {
                Description = userInfo.Description;
            }
            else
            {
                Description = "Description";
            }

            if (userInfo.IdMbtiType != null)
            {
                var mbtiType = await HttpCommands.Command.GetMbtiType(userInfo.IdMbtiType.Value);
                SetMbtiType(mbtiType);
            }
            else
            {
                SetDefaultMbtiType();
            }
        }

        private void SetLanguages(List<Language> languages)
        { 
            var learningLanguages = new List<string>();
            var spokenLanguages = new List<string>();
            foreach (var language in languages)
            {
                if (language.IsSpoken) 
                {
                    var newSpokenLanguage = language.LanguageName;
                    spokenLanguages.Add(newSpokenLanguage);
                } 
                else 
                {
                    var newLearningLanguage = language.LanguageName + " " + language.Level;
                    learningLanguages.Add(newLearningLanguage);
                }
            }
            LearningLanguages = learningLanguages;
            SpokenLanguages = spokenLanguages;
        }

        private void SetMbtiType(MbtiType mbtiType)
        {
            if (!string.IsNullOrWhiteSpace(mbtiType.Type))
            {
                MbtiType = mbtiType.Type;
            }
            else
            {
                MbtiType = "ISFP";
            }

            if (!string.IsNullOrWhiteSpace(mbtiType.Description))
            {
                MbtiDescription = mbtiType.Description;
            }
            else
            {
                MbtiDescription = "ISFP (Adventurer) is a personality type with the Introverted, Observant, Feeling, and Prospecting traits. They tend to have open minds, approaching life, new experiences, and people with grounded warmth. Their ability to stay in the moment helps them uncover exciting potentials.";
            }
        }

        private void SetDefaultUserInfo()
        {
            Age = 20;
            FullName = "John Doe";
            Country = "Ukraine";
            Description = "Description";
            Gender = "Male";
        }

        private void SetDefaultHobbies()
        {
            Hobbies = new List<string>();
        }

        private void SetDefaultLanguages()
        {
            SpokenLanguages = new List<string>();
            LearningLanguages = new List<string>();
        }

        private void SetDefaultMbtiType()
        {
            MbtiType = "ISFP";
            MbtiDescription = "ISFP (Adventurer) is a personality type with the Introverted, Observant, Feeling, and Prospecting traits. They tend to have open minds, approaching life, new experiences, and people with grounded warmth. Their ability to stay in the moment helps them uncover exciting potentials.";
        }

        #endregion

        #region Properties

        public string FullName
        {
            get => _model.FullName;
            set
            {
                if (_model.FullName != value)
                {
                    _model.FullName = value;
                    OnPropertyChanged(nameof(FullName));
                }
            }
        }

        public int Age
        {
            get => _model.Age;
            set
            {
                if (_model.Age != value)
                {
                    _model.Age = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Country
        {
            get => _model.Country;
            set
            {
                if (_model.Country != value)
                {
                    _model.Country = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Gender
        {
            get => _model.Gender;
            set
            {
                if (_model.Gender != value)
                {
                    _model.Gender = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Description
        {
            get => _model.Description;
            set
            {
                if (_model.Description != value)
                {
                    _model.Description = value;
                    OnPropertyChanged();
                }
            }
        }

        public string MbtiType
        {
            get => _model.MbtiType;
            set
            {
                if (_model.MbtiType != value)
                {
                    _model.MbtiType = value;
                    OnPropertyChanged();
                }
            }
        }

        public string MbtiDescription
        {
            get => _model.MbtiDescription;
            set
            {
                if (_model.MbtiDescription != value)
                {
                    _model.MbtiDescription = value;
                    OnPropertyChanged();
                }
            }
        }

        public List<string> Hobbies
        {
            get => _model.Hobbies;
            set
            {
                if (_model.Hobbies != value)
                {
                    _model.Hobbies = value;
                    OnPropertyChanged();
                }
            }
        }

        public List<string> SpokenLanguages
        {
            get => _model.SpokenLanguages;
            set
            {
                if (_model.SpokenLanguages != value)
                {
                    _model.SpokenLanguages = value;
                    OnPropertyChanged();
                }
            }
        }

        public List<string> LearningLanguages
        {
            get => _model.LearningLanguages;
            set
            {
                if (_model.LearningLanguages != value)
                {
                    _model.LearningLanguages = value;
                    OnPropertyChanged();
                }
            }
        }

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


        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion

        #region Switch info/hobbies

        private readonly Color ActiveColor;
        private readonly Color InactiveColor;

        private Color _hobbyColor;
        public Color HobbyColor
        {
            get => _hobbyColor;
            set
            {
                _hobbyColor = value;
                OnPropertyChanged();
            }
        }
        private Color _infoColor;
        public Color InfoColor
        {
            get => _infoColor;
            set
            {
                _infoColor = value;
                OnPropertyChanged();
            }
        }

        private bool _isInfoActive = true;
        public bool IsInfoActive
        {
            get => _isInfoActive;
            set
            {
                _isInfoActive = value;
                OnPropertyChanged();
            }
        }

        private bool _isHobbiesActive = false;
        public bool IsHobbiesActive
        {
            get => _isHobbiesActive;
            set
            {
                _isHobbiesActive = value;
                OnPropertyChanged();
            }
        }

        public ICommand ShowHobbiesCommand => new Command(() =>
        {
            HobbyColor = ActiveColor;
            InfoColor = InactiveColor;
            IsInfoActive = false;
            IsHobbiesActive = true;
        });

        public ICommand ShowInfoCommand => new Command(() =>
        {
            HobbyColor = InactiveColor;
            InfoColor = ActiveColor;
            IsInfoActive = true;
            IsHobbiesActive = false;
        });

        #endregion

        #region Change something views commands
        public ICommand OnChangeProfileCommand => new Command(async () =>
        {
            await Shell.Current.GoToAsync(nameof(ChangeProfilePage));
        });

        public ICommand OnChangeMbtiTypeCommand => new Command(async () =>
        {
            await Shell.Current.GoToAsync(nameof(ChangeMbtiTypePage));
        });

        public ICommand OnChangeHobbiesCommand => new Command(async () =>
        {
            await Shell.Current.GoToAsync(nameof(ChangeHobbiesPage));
        });

        public ICommand OnChangeLanguagesCommand => new Command(async () =>
        {
            await Shell.Current.GoToAsync(nameof(ChangeLanguagesPage));
        });



        #endregion

        #region Bottom view commands

        public ICommand OnChatsCommand => new Command(async () =>
        {
            var idUser = _tokenService.IdUser;
            await ChangeViewCommand.ChangeViewCommand.ChangeOnChatsView(idUser); 
        });

        public ICommand OnSelectPeopleCommand => new Command(async () => 
        {
            await ChangeViewCommand.ChangeViewCommand.ChangeOnSelectPeopleView(null);
        });

        public ICommand OnProfileCommand => new Command(async () =>
        {
            var idUser = _tokenService.IdUser;
            await ChangeViewCommand.ChangeViewCommand.ChangeOnProfileView(idUser);
        });



        #endregion
    }
}
