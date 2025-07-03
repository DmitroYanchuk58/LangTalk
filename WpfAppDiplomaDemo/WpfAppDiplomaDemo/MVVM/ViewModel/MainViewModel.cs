using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAppDiplomaDemo.Command;
using WpfAppDiplomaDemo.Core;
using WpfAppDiplomaDemo.HttpCommands;
using WpfAppDiplomaDemo.MVVM.Model;
using WpfAppDiplomaDemo.MVVM.View;
using WpfAppDiplomaDemo.SignalR;

namespace WpfAppDiplomaDemo.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        private string _token;

        public string Token
        {
            get { return _token; }
            set
            {
                _token = value;
                IdCurrentUser = GetUserIdFromToken(_token).Value;
            }
        }


        public RelayCommand HomeViewCommand { get; set; }

        public HomeViewModel HomeVm { get; set; }

        public LoginViewModel LoginVm { get; set; }

        public RelayCommand LoginViewCommand { get; set; }

        public ChatsViewModel ChatsVm { get; set; }

        public RelayCommand ChatsViewCommand { get; set; }

        public ChatViewModel ChatVm { get; set; }

        public RelayCommand ChatViewCommand { get; set; }

        public RelayCommand SelectPeopleCommand { get; set; }

        public SelectPeopleViewModel SelectPeopleVm { get; set; }

        public ProfileViewModel ProfileVm { get; set; }

        public RelayCommand ProfileViewCommand { get; set; }

        public BottomMenuViewModel BottomMenuVm { get; set; }

        public RegistrationViewModel RegistrationVm { get; set; }

        public RelayCommand RegistrationViewCommand { get; set; }

        public ChangeInfoViewModel ChangeInfoVm { get; set; }

        public RelayCommand ChangeInfoViewCommand { get; set; }

        public ChangeChoiceViewModel ChangeChoiceVm { get; set; }

        public RelayCommand ChangeChoiceViewCommand { get; set; }

        public AddUserLanguageViewModel AddUserLanguageVm { get; set; }

        public RelayCommand AddUserLanguageViewCommand { get; set; }

        public AddUserHobbyViewModel AddUserHobbyVm { get; set; }

        public RelayCommand AddUserHobbyViewCommand { get; set; }

        private Guid _idCurrentUser;
        public Guid IdCurrentUser {
            get 
            { 
                return _idCurrentUser;
            }
            set {
                _idCurrentUser = value;
                CreateChangingSystem();
            }
        }

        private ChangingSystem ChangingSystem;

        private Client ChatClient;

        private object currentView;

        public object CurrentView
        {
            get { return currentView; }
            set
            {
                currentView = value;
                OnPropertyChanged();
            }
        }
        public MainViewModel()
        {
            HomeVm = new HomeViewModel();
            LoginVm = new LoginViewModel(this);
            BottomMenuVm = new BottomMenuViewModel(this);
            RegistrationVm = new RegistrationViewModel(this);
            ChangeChoiceVm = new ChangeChoiceViewModel(this);
            CurrentView = LoginVm;

            RegistrationViewCommand = new RelayCommand(o =>
            {
                CurrentView = RegistrationVm;
            });

            LoginViewCommand = new RelayCommand(o =>
            {
                CurrentView = LoginVm;
            });

            ChangeChoiceViewCommand = new RelayCommand(o =>
            {
                CurrentView = ChangeChoiceVm;
            });

            CreateSelectPeopleViewCommand();
            CreateLoginViewCommand();
        }

        public void CreateChatClient()
        {
            ChatClient = new Client();
        }

        public Client GetClient()
        {
            return ChatClient;
        }

        public SmallChatModel CreateSmallChat(Guid id, string nickname, string lastMessage, DateTime time)
        {
            SmallChatModel smallChatModel = new SmallChatModel()
            {
                IdChat = id,
                Nickname = nickname,
                LastMessage = lastMessage,
                Time = time
            };
            return smallChatModel;
        }

        public Guid? GetUserIdFromToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return null;

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var idClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub");
            var id = Guid.Parse(idClaim?.Value ?? string.Empty);
            return id;
        }

        private void CreateLoginViewCommand()
        {
            LoginViewCommand = new RelayCommand(o =>
            {
                CurrentView = LoginVm;
            });
        }

        private void CreateSelectPeopleViewCommand()
        {
            ChatsViewCommand = new RelayCommand(o =>
            {
                CurrentView = SelectPeopleCommand;
            });
        }

        public void RunChatsViewCommand(ChatsModel model)
        {
            ChatsVm = new ChatsViewModel(model, this);
            ChatsViewCommand = new RelayCommand(o =>
            {
                CurrentView = ChatsVm;
            });
            ChatsViewCommand.Execute(null);
        }

        public void RunChatViewCommand(ChatModel model, Client client)
        {
            ChatVm = new ChatViewModel(model, this, client);
            ChatViewCommand = new RelayCommand(o =>
            {
                CurrentView = ChatVm;
            });
            ChatViewCommand.Execute(null);
        }

        public void RunProfileViewCommand(ProfileModel model)
        {
            ProfileVm = new ProfileViewModel(model, this);
            SelectPeopleCommand = new RelayCommand(o =>
            {
                CurrentView = ProfileVm;
            });
            SelectPeopleCommand.Execute(null);
        }

        public async Task<Guid> SelectPeopleGoNextId()
        {
            return await ChangingSystem.GoNext();
        }

        public async Task<Guid> GetUserId()
        {
            return await ChangingSystem.GetId();
        }

        public void RunSelectPeopleViewCommand(SelectPeopleModel model)
        {
            SelectPeopleVm = new SelectPeopleViewModel(model, this);
            SelectPeopleCommand = new RelayCommand(o =>
            {
                CurrentView = SelectPeopleVm;
            });
            SelectPeopleCommand.Execute(null);
        }

        public void RunChangeInfoViewCommand()
        {
            var mbtiList = Commands.GetAllMbtiTypes();
            var genders = Commands.GetAllGenders();
            ChangeInfoVm = new ChangeInfoViewModel(this, mbtiList.Result, genders.Result);
            ChangeInfoViewCommand = new RelayCommand(o =>
            {
                CurrentView = ChangeInfoVm;
            });
            ChangeInfoViewCommand.Execute(null);
        }

        public void RunAddUserLanguageViewCommand()
        {
            List<string> languages = Commands.GetAllLanguages().Result;
            AddUserLanguageModel model = new AddUserLanguageModel(languages);
            AddUserLanguageVm = new AddUserLanguageViewModel(this, model);
            AddUserLanguageViewCommand = new RelayCommand(o =>
            {
                CurrentView = AddUserLanguageVm;
            });
            AddUserLanguageViewCommand.Execute(null);
        }

        public void RunAddUserHobbyViewCommand()
        {
            List<string> hobbies = Commands.GetAllHobbies().Result;
            AddUserHobbyVm = new AddUserHobbyViewModel(this, hobbies);
            AddUserHobbyViewCommand = new RelayCommand(o =>
            {
                CurrentView = AddUserHobbyVm;
            });
            AddUserHobbyViewCommand.Execute(null);
        }

        private async Task CreateChangingSystem()
        {
            var idUser = IdCurrentUser;
            ChangingSystem = new ChangingSystem(idUser);
            await ChangingSystem.SetIds();
        }
    }
}
