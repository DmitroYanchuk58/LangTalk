using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAppDiplomaDemo.Command;
using WpfAppDiplomaDemo.HttpCommands;
using WpfAppDiplomaDemo.MVVM.Model;

namespace WpfAppDiplomaDemo.MVVM.ViewModel
{
    public class RegistrationViewModel
    {
        private MainViewModel mainViewModel;

        public RegistrationViewModel(MainViewModel mainVM)
        {
            mainViewModel = mainVM;
        }
        public async Task ChangeView()
        {
            await ChangeViewCommand.ChangeViewToChats(mainViewModel);
        }

        public void ChangeViewToLogin()
        {
            mainViewModel.LoginViewCommand.Execute(null);
        }

        public void SetToken(string token)
        {
            mainViewModel.Token = token;
        }
    }
}
