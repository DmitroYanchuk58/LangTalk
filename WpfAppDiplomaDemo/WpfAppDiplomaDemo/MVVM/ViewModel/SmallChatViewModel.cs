using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WpfAppDiplomaDemo.Core;
using WpfAppDiplomaDemo.MVVM.Model;
using WpfAppDiplomaDemo.HttpCommands;

namespace WpfAppDiplomaDemo.MVVM.ViewModel
{
    public class SmallChatViewModel : INotifyPropertyChanged
    {
        private SmallChatModel _model;
        private MainViewModel mainViewModel;

        public SmallChatViewModel(MainViewModel mainVM, SmallChatModel model)
        {
            mainViewModel = mainVM;
            _model = model;
        }

        public async void ChangeView()
        {
            var chat = await Commands.GetChat(IdChat);

            ShowChat(chat);
        }

        public void ShowChat(ChatModel chat)
        {
            Command.ChangeViewCommand.ChangeToChat(mainViewModel, chat);
        }

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

        public string LastMessage
        {
            get => _model.LastMessage;
            set
            {
                if (_model.LastMessage != value)
                {
                    _model.LastMessage = value;
                    OnPropertyChanged();
                }
            }
        }

        public Guid IdChat
        {
            get => _model.IdChat;
            set
            {
                if (_model.IdChat != value)
                {
                    _model.IdChat = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime Time
        {
            get => _model.Time;
            set
            {
                if (_model.Time != value)
                {
                    _model.Time = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
