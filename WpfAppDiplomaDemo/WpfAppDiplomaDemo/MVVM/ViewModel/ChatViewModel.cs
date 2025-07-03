using System;
using System.Collections.ObjectModel;
using System.Windows;
using WpfAppDiplomaDemo.MVVM.Model;
using WpfAppDiplomaDemo.Core;
using WpfAppDiplomaDemo.SignalR;

namespace WpfAppDiplomaDemo.MVVM.ViewModel
{
    public class ChatViewModel : ObservableObject
    {
        private ChatModel _model;
        public BottomMenuViewModel BottomMenuVM { get; }
        public MainViewModel MainVM { get; set; }

        public Client Client { get; }

        public ChatViewModel(ChatModel model, MainViewModel mainModel, Client client)
        {
            _model = model;
            MainVM = mainModel;
            Messages = new ObservableCollection<MessageModel>(_model.Messages ?? new List<MessageModel>());
            BottomMenuVM = new BottomMenuViewModel(mainModel);
            Client = client;
            Client.RegisterHandlers(OnMessageReceived);

            Client.JoinServer(IdChat).ConfigureAwait(false); 
        }

        private void OnMessageReceived(MessageModel message)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Messages.Add(new MessageModel
                {
                    Text = message.Text,
                    TimeSend = message.TimeSend,
                    UserName = message.UserName,
                    IdMessage = message.IdMessage,
                    IdUser = message.IdUser,
                });
            });
        }

        public async void SendMessage(string textMessage)
        {
            var idUser = MainVM.GetUserIdFromToken(MainVM.Token);
            await Client.SendMessage(IdChat, idUser.Value, textMessage);
        }

        public Guid IdChat
        {
            get => _model.IdChat;
            set
            {
                if (_model.IdChat != value)
                {
                    _model.IdChat = value;
                    OnPropertyChanged(nameof(IdChat));
                }
            }
        }

        public string ChatName
        {
            get => _model.ChatName;
            set
            {
                if (_model.ChatName != value)
                {
                    _model.ChatName = value;
                    OnPropertyChanged(nameof(ChatName));
                }
            }
        }

        private ObservableCollection<MessageModel> _messages;
        public ObservableCollection<MessageModel> Messages
        {
            get => _messages;
            set
            {
                if (_messages != value)
                {
                    _messages = value;
                    OnPropertyChanged(nameof(Messages));
                }
            }
        }
    }
}