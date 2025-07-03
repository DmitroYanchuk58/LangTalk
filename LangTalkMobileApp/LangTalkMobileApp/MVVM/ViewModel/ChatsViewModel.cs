using LangTalkMobileApp.DTOs;
using LangTalkMobileApp.MVVM.View;
using LangTalkMobileApp.Token;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LangTalkMobileApp.MVVM.ViewModel
{
    public class ChatsViewModel : INotifyPropertyChanged, IQueryAttributable
    {
        private Guid IdUser { get; set; }
        private TokenService _tokenService;

        public ChatsViewModel(TokenService tokenService)
        {
            _tokenService = tokenService;
            Chats = new();
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query != null)
            {
                IdUser = Guid.Parse(query["IdUser"].ToString());
                OnPropertyChanged(nameof(IdUser));

                await LoadChats(IdUser);
            }
        }

        private async Task LoadChats(Guid idUser)
        {
            var chats = await HttpCommands.Command.GetChats(idUser);
            foreach (var chat in chats)
            {
                Chats.Add(chat);
            }
        }

        #region Properties
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private ObservableCollection<SmallChat> chats;
        public ObservableCollection<SmallChat> Chats
        {
            get => chats;
            set
            {
                if (chats != value)
                {
                    chats = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion
        public ICommand ChatTap => new Command<Guid>(OnChatTapped);

        private async void OnChatTapped(Guid idChat)
        {
            await ChangeViewCommand.ChangeViewCommand.ChangeOnChatView(idChat);
        }


        #region Bottom view command

        public ICommand OnProfileCommand => new Command(async () =>
        {
            var idUser = _tokenService.IdUser;
            await ChangeViewCommand.ChangeViewCommand.ChangeOnProfileView(idUser);
        });

        public ICommand OnSelectPeopleCommand => new Command(async () =>
        {
            await ChangeViewCommand.ChangeViewCommand.ChangeOnSelectPeopleView(null);
        });

        #endregion

    }
}
