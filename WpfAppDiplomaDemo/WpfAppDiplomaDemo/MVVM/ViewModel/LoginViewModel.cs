using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAppDiplomaDemo.Command;
using WpfAppDiplomaDemo.Core;
using WpfAppDiplomaDemo.HttpCommands;
using WpfAppDiplomaDemo.MVVM.Model;

namespace WpfAppDiplomaDemo.MVVM.ViewModel
{
    public class LoginViewModel
    {
        private MainViewModel mainViewModel;

        public LoginViewModel(MainViewModel mainVM)
        {
            mainViewModel = mainVM;
        }
        public async Task ChangeView()
        {
            await ChangeViewCommand.ChangeViewToChats(mainViewModel);
        }

        public void ChangeViewToRegistration()
        {
            mainViewModel.RegistrationViewCommand.Execute(null);
        }

        public void SetToken(string token)
        {
            mainViewModel.Token = token;
        }
    }
}
