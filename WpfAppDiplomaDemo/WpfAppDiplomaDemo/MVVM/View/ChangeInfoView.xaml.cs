using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfAppDiplomaDemo.HttpCommands;
using WpfAppDiplomaDemo.MVVM.ViewModel;

namespace WpfAppDiplomaDemo.MVVM.View
{
    /// <summary>
    /// Interaction logic for ChangeInfoView.xaml
    /// </summary>
    public partial class ChangeInfoView : UserControl
    {
        public ChangeInfoView()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await UpdateUser();
            await UpdateUserInfo();
            await UpdateMbtiType();

            var vm = (ChangeInfoViewModel)DataContext;
            vm.ChangeViewToProfile();
        }

        private async Task UpdateUser()
        {
            var nickname = NicknameTb.Text;
            var vm = (ChangeInfoViewModel)DataContext;
            if (!string.IsNullOrWhiteSpace(nickname))
            {
                await vm.ChangeNickname(nickname);
            }
        }

        private async Task UpdateUserInfo()
        {
            var vm = (ChangeInfoViewModel)DataContext;
            var description = DescriptionTb.Text;
            var firstName = FirstNameTb.Text;
            var lastName = SecondNameTb.Text;
            var gender = GenderTb.SelectedItem as string;
            var dateOfBirth = Date.SelectedDate ?? DateTime.MinValue;
            if (dateOfBirth != null && !string.IsNullOrWhiteSpace(description) && !string.IsNullOrWhiteSpace(firstName)
                && !string.IsNullOrWhiteSpace(lastName) && !string.IsNullOrWhiteSpace(gender))
            {
                await vm.ChangeUserInfo(gender, description, firstName, lastName, "Japan", dateOfBirth);
            }
        }

        private async Task UpdateMbtiType()
        {
            var vm = (ChangeInfoViewModel)DataContext;
            var mbtiType = MbtiTb.SelectedItem as string;
            if (mbtiType != null &&!string.IsNullOrWhiteSpace(mbtiType))
            {
                await vm.ChangeMbtiType(mbtiType);
            }
        }

    }
}
