using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfAppDiplomaDemo.MVVM.ViewModel
{
    public class ChangeChoiceViewModel
    {
        private MainViewModel _mainViewModel;
        public ChangeChoiceViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        public void ChangeInfoViewShow()
        {
            _mainViewModel.RunChangeInfoViewCommand();
        }

        public void AddUserLanguageViewShow()
        {
            _mainViewModel.RunAddUserLanguageViewCommand();
        }

        public void AddUserHobbyViewShow()
        {
            _mainViewModel.RunAddUserHobbyViewCommand();
        }
    }
}
