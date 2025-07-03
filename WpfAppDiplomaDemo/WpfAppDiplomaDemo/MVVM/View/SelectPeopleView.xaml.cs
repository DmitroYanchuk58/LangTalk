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
using WpfAppDiplomaDemo.HttpCommands;
using WpfAppDiplomaDemo.MVVM.Model;
using WpfAppDiplomaDemo.MVVM.ViewModel;

namespace WpfAppDiplomaDemo.MVVM.View
{
    /// <summary>
    /// Interaction logic for SelectPeopleView.xaml
    /// </summary>
    public partial class SelectPeopleView : UserControl
    {
        public SelectPeopleView()
        {
            InitializeComponent();
        }

        public SelectPeopleView(SelectPeopleViewModel view)
        {
            InitializeComponent();
            DataContext = view;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var vm = (SelectPeopleViewModel)DataContext;
            vm.BottomMenuVM.GoNext();   
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var vm =(SelectPeopleViewModel)DataContext;
            var idChat = await vm.CreateChat();
            var chat = await Commands.GetChat(idChat);
            vm.ShowChat(chat);
        }
    }
}
