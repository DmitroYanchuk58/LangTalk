using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAppDiplomaDemo.Command;
using WpfAppDiplomaDemo.HttpCommands;
using WpfAppDiplomaDemo.MVVM.Model;

namespace WpfAppDiplomaDemo.MVVM.ViewModel
{
    public class ChangeInfoViewModel
    {
        public MainViewModel MainVM { get; set; }

        public BottomMenuViewModel BottomMenuVM => new BottomMenuViewModel(MainVM);

        public ChangeInfoViewModel(MainViewModel mainViewModel, List<string> mbtiList,List<string> genders)
        {
            MainVM = mainViewModel;
            MbtiTypeList = mbtiList;
            GenderList = genders;
        }

        public List<string> MbtiTypeList { get; set; } = new List<string>();


        public List<string> GenderList { get; set; } = new List<string>();

        public async Task ChangeNickname(string nickname)
        {
            await Commands.ChangeNickname(MainVM.GetUserIdFromToken(MainVM.Token) ?? Guid.Empty, nickname);
        }

        public async void ChangeViewToProfile()
        {
            await ChangeViewCommand.ChangeViewToProfile(MainVM);
        }

        public async Task ChangeUserInfo(string newGender,
            string description, string firstName, string lastName, string country,
            DateTime dateOfBirth)
        {
            Guid idUser = MainVM.GetUserIdFromToken(MainVM.Token).Value;
            await Commands.ChangeUserInfo(idUser, newGender, description, firstName, lastName, country, dateOfBirth);
        }

        public async Task ChangeMbtiType(string mbtiType)
        {
            var idUser = MainVM.GetUserIdFromToken(MainVM.Token).Value;
            var idUserInfoString = await Commands.GetUserInfoIdByIdUser(idUser);
            Guid idUserInfo = Guid.Parse(idUserInfoString.Trim('"'));
            await Commands.ChangeMbtiType(idUserInfo, mbtiType);
        }
    }
}
