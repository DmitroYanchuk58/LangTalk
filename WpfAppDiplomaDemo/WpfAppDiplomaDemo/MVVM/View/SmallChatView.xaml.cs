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
    /// Interaction logic for SmallChatView.xaml
    /// </summary>
    public partial class SmallChatView : UserControl
    {
        public SmallChatView()
        {
            InitializeComponent();
        }

        //Show Chat
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var vm = (SmallChatViewModel)DataContext;
            vm.ChangeView();
        }
    }
}
