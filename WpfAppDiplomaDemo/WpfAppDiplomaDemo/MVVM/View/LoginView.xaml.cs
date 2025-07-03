using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using System.Threading.Tasks;
using WpfAppDiplomaDemo.MVVM.ViewModel;
using WpfAppDiplomaDemo.HttpCommands;

namespace WpfAppDiplomaDemo.MVVM.View
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var vm = (LoginViewModel)DataContext;

            var email = EmailBox.Text;
            var password = PasswordBox.Text;
            try
            {
                var token = await HttpCommands.Commands.LoginGetToken(email, password);
                vm.SetToken(token);
                await vm.ChangeView();
            }
            catch
            {
                WrongDataTextBlock.Visibility = Visibility.Visible;
            }

        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var vm = (LoginViewModel)DataContext;
            vm.ChangeViewToRegistration();
        }
    }
}
