using LangTalkMobileApp.ChangeViewCommand;
using LangTalkMobileApp.DTOs;
using LangTalkMobileApp.HttpCommands;
using LangTalkMobileApp.Token;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LangTalkMobileApp.MVVM.ViewModel
{
    public class ChangeMbtiTypeViewModel : INotifyPropertyChanged
    {
        private TokenService _tokenService;
        private List<MbtiType> _mbtiTypeDtos;
        public ChangeMbtiTypeViewModel(TokenService tokenService)
        {
            _tokenService = tokenService;
            Task.Run(async () =>
            {
                var mbtiTypes = await LangTalkMobileApp.HttpCommands.Command.GetAllMbtiTypes();
                _mbtiTypeDtos = mbtiTypes;
                MbtiTypes = _mbtiTypeDtos.Select(x => x.Type).ToList();
                OnPropertyChanged(nameof(MbtiTypes));
            });
        }

        #region Properties
        private List<string> _mbtiTypes;
        public List<string> MbtiTypes
        {
            get => _mbtiTypes;
            set
            {
                _mbtiTypes = value;
                OnPropertyChanged(nameof(MbtiTypes));
            }
        }

        private string _selectedMbtiType;
        public string MbtiType
        {
            get => _selectedMbtiType;
            set
            {
                if (_selectedMbtiType != value)
                {
                    _selectedMbtiType = value;
                    OnPropertyChanged(nameof(MbtiType));

                    var selected = _mbtiTypeDtos.FirstOrDefault(x => x.Type == _selectedMbtiType);
                    MbtiDescription = selected?.Description ?? string.Empty;
                }
            }
        }

        private string _mbtiDescription;
        public string MbtiDescription
        {
            get => _mbtiDescription;
            set
            {
                if (_mbtiDescription != value)
                {
                    _mbtiDescription = value;
                    OnPropertyChanged(nameof(MbtiDescription));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion

        public ICommand OnProfileCommand => new Microsoft.Maui.Controls.Command(async () =>
        {
            var idUser = _tokenService.IdUser;
            await ChangeViewCommand.ChangeViewCommand.ChangeOnProfileView(idUser);
        });

        public ICommand ChangeMbtiTypeCommand => new Microsoft.Maui.Controls.Command(async () =>
        {
            var idUser = _tokenService.IdUser;
            await LangTalkMobileApp.HttpCommands.Command.ChangeMbtiType(idUser, MbtiType);
            await ChangeViewCommand.ChangeViewCommand.ChangeOnProfileView(idUser);
        });
    }
}
