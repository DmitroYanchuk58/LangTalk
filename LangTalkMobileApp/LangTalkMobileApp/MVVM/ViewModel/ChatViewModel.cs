using LangTalkMobileApp.DTOs;
using LangTalkMobileApp.SignalR;
using LangTalkMobileApp.Token;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using System.Windows.Input;
using Microsoft.Maui.ApplicationModel;

namespace LangTalkMobileApp.MVVM.ViewModel
{
    public class ChatViewModel : INotifyPropertyChanged, IQueryAttributable
    {
        private Guid IdChat;
        private TokenService _tokenService;
        public Client Client { get; }

        public ChatViewModel(TokenService tokenService)
        {
            _tokenService = tokenService;
            Messages = new();
            Client = new Client();
            Client.RegisterHandlers(OnMessageReceived);
        }

        private void OnMessageReceived(Message message)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Messages.Insert(0, new Message
                {
                    Text = message.Text,
                    TimeSend = message.TimeSend,
                    UserName = message.UserName,
                    IdMessage = message.IdMessage,
                    IdUser = message.IdUser,
                });
            });
        }


        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query != null)
            {
                IdChat = Guid.Parse(query["IdChat"].ToString());
                OnPropertyChanged(nameof(IdChat));

                await Client.JoinServer(IdChat).ConfigureAwait(false);
                await LoadChat(IdChat);
            }
        }

        private async Task LoadChat(Guid idChat)
        {
            var chat = await HttpCommands.Command.GetChat(idChat);

            MainThread.BeginInvokeOnMainThread(() =>
            {
                ChatName = chat.ChatName;
                foreach (var message in chat.Messages)
                {
                    Message newMessage = new Message()
                    {
                        IdMessage = message.IdMessage,
                        UserName = message.UserName,
                        Text = message.Text,
                        TimeSend = message.TimeSend,
                        IdUser = message.IdUser
                    };

                    Messages.Add(newMessage);
                }
            });
        }

        public ICommand OnChatsCommand => new Command(async () =>
        {
            var idUser = _tokenService.IdUser;
            await ChangeViewCommand.ChangeViewCommand.ChangeOnChatsView(idUser);
        });

        #region Properties

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private string chatName;

        public string ChatName
        {
            get => chatName;
            set
            {
                if (chatName != value)
                {
                    chatName = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<Message> messages;

        public ObservableCollection<Message> Messages
        {
            get => messages;
            set
            {
                if (messages != value)
                {
                    messages = value;
                    OnPropertyChanged();
                }
            }
        }

        private string inputText;
        public string InputText
        {
            get => inputText;
            set
            {
                if (inputText != value)
                {
                    inputText = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        public async Task SendMessage(string textMessage)
        {
            var idUser = _tokenService.IdUser;
            await Client.SendMessage(IdChat, idUser, textMessage);
        }
        public ICommand SendCommand => new Command(async() =>
        {
            if (!string.IsNullOrWhiteSpace(InputText))
            {
                await SendMessage(InputText);
                InputText = "";
            }
        });
    }
}
