using System;
using System.Collections.Generic;
using System.Linq;
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
using WpfAppDiplomaDemo.MVVM.ViewModel;

namespace WpfAppDiplomaDemo.MVVM.View
{
    /// <summary>
    /// Interaction logic for RegistrationView.xaml
    /// </summary>
    public partial class RegistrationView : UserControl
    {
        public RegistrationView()
        {
            InitializeComponent();
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var vm = (RegistrationViewModel)DataContext;
            vm.ChangeViewToLogin();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var vm = (RegistrationViewModel)DataContext;
            string email = EmailTb.Text;
            string password = PasswordTb.Text;
            string confirmPassword = ConfirmPasswordTb.Text;
            string nickname = NicknameTb.Text;
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword) || string.IsNullOrEmpty(nickname) || password != confirmPassword)
            {
                return;
            }
            try
            {
                var token = await HttpCommands.Commands.RegistrationAndGetToken(email, password, nickname);
                vm.SetToken(token);
                await vm.ChangeView();
            }
            catch
            {
                //TODO: visible error
            }
        }
    }
}
