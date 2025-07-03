using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAppDiplomaDemo.Command;
using WpfAppDiplomaDemo.HttpCommands;
using WpfAppDiplomaDemo.MVVM.Model;
using WpfAppDiplomaDemo.SignalR;

namespace WpfAppDiplomaDemo.MVVM.ViewModel
{
    public class BottomMenuViewModel
    {
        private MainViewModel _mainViewModel;

        public BottomMenuViewModel(MainViewModel model)
        {
            _mainViewModel = model;
        }

        public async void ChangeViewToProfile()
        {
            await ChangeViewCommand.ChangeViewToProfile(_mainViewModel);
        }

        public async Task<Guid> GetId()
        {
            return await _mainViewModel.GetUserId();
        }

        public async void GoNext()
        {
            Guid idUser = await _mainViewModel.SelectPeopleGoNextId();
            await ChangeViewToSelectPeople(idUser);
        }

        public async void ChangeViewToChats()
        {
            var id = _mainViewModel.GetUserIdFromToken(_mainViewModel.Token);
            var response = await HttpCommand.SendGetHttpRequest($"/Chat/GetChats?idUser={id}", _mainViewModel.Token);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var smallChats = System.Text.Json.JsonSerializer.Deserialize<List<SmallChatModel>>(json) ?? new List<SmallChatModel>();

                var model = new ChatsModel(smallChats, _mainViewModel);
                _mainViewModel.RunChatsViewCommand(model);
                if (_mainViewModel == null) return;
            }
        }

        public async Task ChangeViewToSelectPeople(Guid idUser)
        {
            await ChangeViewCommand.ChangeViewToSelectPeople(_mainViewModel, idUser);
        }

        public void ChangeViewToChangeInfo()
        {
            if (_mainViewModel == null) return;
            _mainViewModel.ChangeChoiceViewCommand.Execute(null); 
        }
    }
}
