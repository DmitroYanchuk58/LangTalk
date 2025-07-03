using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAppDiplomaDemo.Command;
using WpfAppDiplomaDemo.HttpCommands;

namespace WpfAppDiplomaDemo.MVVM.ViewModel
{
    public class AddUserHobbyViewModel
    {
        private MainViewModel MainVm;
        public AddUserHobbyViewModel(MainViewModel mainVm, List<string> hobbies)
        {
            MainVm = mainVm;
            Hobbies = hobbies;
        }
        public List<string> Hobbies { get; set; }

        public async void ChangeViewToProfile()
        {
            await ChangeViewCommand.ChangeViewToProfile(MainVm);
        }

        public async void AddHobby(string hobby)
        {
            var idUser = MainVm.GetUserIdFromToken(MainVm.Token);
            var idHobbyString = Commands.GetHobbyIdByName(hobby).Result;
            Guid idHobby = Guid.Parse(idHobbyString.Trim('"'));
            await Commands.CreateUserHobby(idUser.Value, idHobby);
        }
    }
}
