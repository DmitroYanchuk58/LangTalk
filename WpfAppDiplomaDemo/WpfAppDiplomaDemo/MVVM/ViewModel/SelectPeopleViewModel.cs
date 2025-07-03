using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfAppDiplomaDemo.Core;
using WpfAppDiplomaDemo.HttpCommands;
using WpfAppDiplomaDemo.MVVM.Model;

namespace WpfAppDiplomaDemo.MVVM.ViewModel
{
    public class SelectPeopleViewModel : INotifyPropertyChanged
    {
        private string _name;
        private string _mbtiType;
        private string _country;
        private string _description;
        private List<string> _hobbies;
        private string _sameHobbiesCount;
        private bool _isMbtiTypeCompatible;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string MbtiType
        {
            get => _mbtiType;
            set
            {
                _mbtiType = value;
                OnPropertyChanged();
            }
        }

        public string Country
        {
            get => _country;
            set
            {
                _country = value;
                OnPropertyChanged();
            }
        }

        public string Description 
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        public List<string> Hobbies
        {
            get => _hobbies;
            set
            {
                _hobbies = value;
                OnPropertyChanged();
            }
        }

        public string SameHobbiesCount 
        {
            get => _sameHobbiesCount;
            set
            {
                _sameHobbiesCount = value;
                OnPropertyChanged();
            }
        }

        public bool IsMbtiTypeCompatible
        {
            get => _isMbtiTypeCompatible;
            set
            {
                _isMbtiTypeCompatible = value;
                OnPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public MainViewModel MainVM { get; set; }

        public BottomMenuViewModel BottomMenuVM => new BottomMenuViewModel(MainVM);

        public SelectPeopleViewModel(SelectPeopleModel model, MainViewModel mainViewModel)
        {
            Name = model.Name;
            MbtiType = model.MbtiType;
            Country = model.Country;
            Description = model.Description;
            Hobbies = model.Hobbies ?? new List<string>();
            SameHobbiesCount = model.SameHobbiesCount;
            IsMbtiTypeCompatible = model.IsMbtiTypeCompatible;
            MainVM = mainViewModel;
        }

        public async Task<Guid> GetIdUser()
        {
            return await MainVM.GetUserId();
        }

        public async void GoNext()
        {
            await MainVM.SelectPeopleGoNextId();
        }

        public async Task<Guid> CreateChat()
        {
            Guid idFirstUser = await GetIdUser();
            Guid idSecondUser = MainVM.GetUserIdFromToken(MainVM.Token).Value;
            var idChat = await Commands.CreateChat(idFirstUser, idSecondUser);
            return idChat;
        }

        public void ShowChat(ChatModel chat)
        {
            Command.ChangeViewCommand.ChangeToChat(MainVM, chat);
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
