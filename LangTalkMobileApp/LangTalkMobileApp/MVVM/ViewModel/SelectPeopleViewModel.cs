using LangTalkMobileApp.ChangeViewCommand;
using LangTalkMobileApp.DTOs;
using LangTalkMobileApp.MVVM.Model;
using LangTalkMobileApp.MVVM.View;
using LangTalkMobileApp.Token;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LangTalkMobileApp.MVVM.ViewModel
{
    public class SelectPeopleViewModel : INotifyPropertyChanged, IQueryAttributable
    {
        private SelectPeopleModel _model;
        public SelectPeopleModel Model
        {
            get => _model;
            set
            {
                if (_model != value)
                {
                    _model = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Nickname));
                    OnPropertyChanged(nameof(FirstName));
                    OnPropertyChanged(nameof(LastName));
                    OnPropertyChanged(nameof(Age));
                    OnPropertyChanged(nameof(Country));
                    OnPropertyChanged(nameof(MbtiType));
                    OnPropertyChanged(nameof(MbtiDescription));
                    OnPropertyChanged(nameof(Hobbies));
                    OnPropertyChanged(nameof(SpokenLanguages));
                    OnPropertyChanged(nameof(LearningLanguages));
                    OnPropertyChanged(nameof(IsCompatibleType));
                    OnPropertyChanged(nameof(DoesHaveSomeHobbies));
                    OnPropertyChanged(nameof(CountSameHobbies));
                    OnPropertyChanged(nameof(SameHobbies));
                    OnPropertyChanged(nameof(DisplayedImage));
                }
            }
        }


        private TokenService _tokenService;
        private SelectPeopleChangingSystem _changingSystem;

        public event PropertyChangedEventHandler PropertyChanged;

        public SelectPeopleViewModel(TokenService tokenService)
        {
            Model = new SelectPeopleModel();
            _tokenService = tokenService;
            var idUser = _tokenService.IdUser;
            _changingSystem = new SelectPeopleChangingSystem(idUser);
            Task.Run(async () =>
            {
                Model = await _changingSystem.SetIds();
            });
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query != null)
            {
                SortPeopleParameters parametrs = query["Parametrs"] as SortPeopleParameters;
                await _changingSystem.SetParametrs(parametrs);
            }
        }

        #region Properties

        public string Nickname
        {
            get => _model.Nickname;
            set
            {
                if (_model.Nickname != value)
                {
                    _model.Nickname = value;
                    OnPropertyChanged();
                }
            }
        }

        public string FirstName
        {
            get => _model.FirstName;
            set
            {
                if (_model.FirstName != value)
                {
                    _model.FirstName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string LastName
        {
            get => _model.LastName;
            set
            {
                if (_model.LastName != value)
                {
                    _model.LastName = value;
                    OnPropertyChanged();
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

        public bool IsCompatibleType
        {
            get => _model.IsCompatibleType;
            set
            {
                if (_model.IsCompatibleType != value)
                {
                    _model.IsCompatibleType = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool DoesHaveSomeHobbies
        {
            get => _model.DoesHaveSomeHobbies;
            set
            {
                if (_model.DoesHaveSomeHobbies != value)
                {
                    _model.DoesHaveSomeHobbies = value;
                    OnPropertyChanged();
                }
            }
        }

        public int CountSameHobbies
        {
            get => _model.CountSameHobbies;
            set
            {
                if (_model.CountSameHobbies != value)
                {
                    _model.CountSameHobbies = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(SameHobbies));
                }
            }
        }

        public ImageSource DisplayedImage
        {
            get => _model.DisplayedImage;
            set
            {
                if (_model.DisplayedImage != value)
                {
                    _model.DisplayedImage = value;
                    OnPropertyChanged(nameof(DisplayedImage));
                }
            }
        }

        public string SameHobbies => $"{CountSameHobbies} same hobbies";


        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion

        public ICommand NextPersonCommand => new Command(async() =>
        {
            Model = await _changingSystem.GoNextUser();
        });
        public ICommand ChatToUserCommand => new Command(async () =>
        {
            var idUser = _tokenService.IdUser;
            //TODO:Check if chat exist
            var idNewChat = await HttpCommands.Command.CreateChat(idUser, _model.IdUser);
            await ChangeViewCommand.ChangeViewCommand.ChangeOnChatView(idNewChat);
        });
            
        public ICommand ParametrsCommand => new Command(async () =>
        {
            await Shell.Current.GoToAsync(nameof(SortPeopleParametrsPage));
        });

        public ICommand OnAnotherProfileCommand => new Command(async () =>
        {
            await ChangeViewCommand.ChangeViewCommand.ChangeOnProfileView(_model.IdUser);
        });

        #region Bottom view command

        public ICommand OnProfileCommand => new Command(async () =>
        {
            var idUser = _tokenService.IdUser;
            await ChangeViewCommand.ChangeViewCommand.ChangeOnProfileView(idUser);
        });

        public ICommand OnChatsCommand => new Command(async () =>
        {
            var idUser = _tokenService.IdUser;
            await ChangeViewCommand.ChangeViewCommand.ChangeOnChatsView(idUser);
        });

        #endregion
    }
}
