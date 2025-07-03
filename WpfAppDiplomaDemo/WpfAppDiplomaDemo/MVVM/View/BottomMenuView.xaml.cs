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
    /// Interaction logic for BottomMenuView.xaml
    /// </summary>
    public partial class BottomMenuView : UserControl
    {
        public BottomMenuView()
        {
            InitializeComponent();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var vm = (BottomMenuViewModel)DataContext;
            vm.ChangeViewToProfile();
        }

        private void Border_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            var vm = (BottomMenuViewModel)DataContext;
            vm.ChangeViewToChats();
        }

        private async void Border_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            var vm = (BottomMenuViewModel)DataContext;
            Guid idUser = await vm.GetId();
            vm.ChangeViewToSelectPeople(idUser);
        }

        private void Border_MouseLeftButtonDown_3(object sender, MouseButtonEventArgs e)
        {
            var vm =(BottomMenuViewModel)DataContext;
            vm.ChangeViewToChangeInfo();
        }
    }
}
